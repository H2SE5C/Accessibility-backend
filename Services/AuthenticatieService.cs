﻿using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend.Modellen.Extra;
using Accessibility_backend.Modellen.Registreermodellen;
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Policy;
using System.Text;
using Response = Accessibility_backend.Modellen.Registreermodellen.Response;

namespace Accessibility_backend.Services
{
    public class AuthenticatieService : IAuthenticatieService
    {
        private readonly UserManager<Gebruiker> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        public AuthenticatieService(
        UserManager<Gebruiker> userManager,
        RoleManager<Rol> roleManager,
        IConfiguration configuration,
        ApplicationDbContext applicationDbContext,
        IEmailSender emailSender,
        IHttpContextAccessor httpContextAccessor,
        IUrlHelperFactory urlHelperFactory,
        IActionContextAccessor actionContextAccessor)
        {
            _context = applicationDbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _emailSender = emailSender;
            _httpContextAccessor = httpContextAccessor;
            _urlHelperFactory = urlHelperFactory;
            _actionContextAccessor = actionContextAccessor;
        }
        public async Task<IActionResult> VerifieerEmail(string token, string email)
        {
            var gebruiker = await _userManager.FindByEmailAsync(email);
            if (gebruiker == null) {

                return new BadRequestObjectResult(new Response{ Status = "Error", Message = "Geen gebruiker gevonden" });
            }
            await _userManager.ConfirmEmailAsync(gebruiker, token);
            return new OkObjectResult("Geverifieerd! U kan dit venster sluiten.");
        }
        public async Task<IActionResult> Login(LoginModel model)
        {
            HttpContext? context = _httpContextAccessor.HttpContext;
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "Geen gebruiker gevonden" });
            };
            if (!user.EmailConfirmed)
            {
                var rolesForUser = await _userManager.GetRolesAsync(user);

                if (rolesForUser[0] == "Bedrijf")
                {
                    return new UnauthorizedObjectResult(new Response { Status = "Error", Message = "Een medewerker moet eerst het account goedkeuren!" });
                }
                return new UnauthorizedObjectResult(new Response { Status = "Error", Message = "Verifieer email eerst!" });
            }
            if (!await _userManager.CheckPasswordAsync(user, model.Wachtwoord))
            {
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "Wachtwoord fout" });
            };

            /*if (user != null && await _userManager.CheckPasswordAsync(user, model.Wachtwoord))
            {*/
            var userRoles = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
                {
                    new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new (ClaimTypes.Email, user.Email),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

            foreach (var userRole in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var token = GetToken(authClaims);

            //refresh token naar db
            var refreshToken = Guid.NewGuid().ToString();
            var refreshExpiry = DateTime.Now.AddDays(7);
            user.RefreshToken = refreshToken;

            _context.Gebruikers.Update(user);
            await _context.SaveChangesAsync();
            context?.Response.Cookies.Append("refresh", refreshToken, new CookieOptions
            {
                Expires = refreshExpiry,
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.None
            });
            return new OkObjectResult(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                roles = userRoles,
            });
            /* }
             return Unauthorized();*/
        }
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        public IActionResult LogUit()
        {
            HttpContext? context = _httpContextAccessor.HttpContext;
            string? token = context.Request.Cookies["refresh"];
            if (token != null)
            {

                context.Response.Cookies.Append("refresh", "", new CookieOptions
                {
                    Expires = DateTime.Now.AddYears(-1),
                    HttpOnly = true,
                    Secure = true,
                    IsEssential = true,
                    SameSite = SameSiteMode.None
                });

                return new OkObjectResult(new Response { Status = "Success", Message = "Uitgelogd!" });
            }

            return new BadRequestObjectResult(new Response { Status = "Error", Message = "Geen token gevonden" });
        }
        public async Task<IActionResult> Refresh()
        {
            HttpContext? context = _httpContextAccessor.HttpContext;
            string? refresh = context.Request.Cookies["refresh"];
            if (refresh != null)
            {
                var user = await _context.Gebruikers.FirstOrDefaultAsync(g => g.RefreshToken == refresh);
                if (user != null)
                {
                    var userRoles = await _userManager.GetRolesAsync(user);

                    //dubbel code van login. splits later op
                    var authClaims = new List<Claim>
                {
                    new (ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new (ClaimTypes.Email, user.Email),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }

                    var newToken = GetToken(authClaims);

                    return new OkObjectResult(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(newToken),
                        expiration = newToken.ValidTo,
                        roles = userRoles,
                    });
                }
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "Geen gebruiker gevonden!" });
            }

            return new BadRequestObjectResult(new Response { Status = "Error", Message = "Geen token gevonden" });
        }
        private async Task<Rol> RolMaak(string rolNaam)
        {
            //kan je gewoon niet var rol gebruiken om te checken of rol al bestaat? if (!rol) {}
            var roleExists = await _roleManager.RoleExistsAsync(rolNaam);
            if (!roleExists)
            {
                var role = new Rol { Naam = rolNaam, Name = rolNaam };
                var res = await _roleManager.CreateAsync(role);
                if (!res.Succeeded)
                {
                    throw new Exception("Role creation failed!");
                }
            }
            var rol = await _context.Rollen.FirstOrDefaultAsync(r => r.Naam == rolNaam);
            return rol;
        }
        private async Task RegistreerGebruikerMetRol(RegistrerenBasis model, string rolNaam)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                throw new Exception("Email bestaat al");
            }

            var rol = await RolMaak(rolNaam);

            Gebruiker user = new()
            {
                UserName = model.Email,
                Email = model.Email,
                Rol = rol,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Wachtwoord);
            if (!result.Succeeded)
            {
                var exceptionText = result.Errors.Aggregate("User Creation Failed - Identity Exception. Errors were: \n\r\n\r", (current, error) => current + (" - " + error + "\n\r"));
                throw new Exception(exceptionText);
            }

            await _userManager.AddToRoleAsync(user, rolNaam);
        }
        public async Task<IActionResult> RegistreerBeheerder(RegistrerenBasis model)
        {
            var rolNaam = "Beheerder";
            await RegistreerGebruikerMetRol(model, rolNaam);
            return new OkObjectResult(new Response { Status = "Success", Message = "Gebruiker aangemaakt!" });
        }
        public async Task<IActionResult> RegistreerDeveloper(RegistrerenBasis model)
        {
            var rolNaam = "Developer";
            await RegistreerGebruikerMetRol(model, rolNaam);
            return new OkObjectResult(new Response { Status = "Success", Message = "Gebruiker aangemaakt!" });
        }
        public async Task<IActionResult> RegistreerMedewerker(RegistrerenMedewerkers model)
        {
            var rolNaam = "Medewerker";
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
            {
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "Email bestaat al!" });
            }

            var rol = await RolMaak(rolNaam);

            Medewerker medewerker = new()
            {
                UserName = model.Email,
                Naam = model.Naam,
                Email = model.Email,
                Rol = rol,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(medewerker, model.Wachtwoord);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "Wachtwoord is verkeerd" });
            }

            await _userManager.AddToRoleAsync(medewerker, rolNaam);
            return new OkObjectResult(new Response { Status = "Success", Message = "User created successfully!" });
        }
        public async Task<IActionResult> RegistreerBedrijf(RegistrerenBedrijf model)
        {
            var rolNaam = "Bedrijf";
            var nameExists = await _context.Bedrijven.Where(b => b.Bedrijfsnaam == model.Bedrijfsnaam).AnyAsync();
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (nameExists)
            {
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "Bedrijf naam wordt al gebruikt!" });
            }
            if (userExists != null)
            {
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "Email bestaat al!" });
            }

            var rol = await RolMaak(rolNaam);

            Bedrijf bedrijf = new()
            {
                UserName = model.Email,
                Bedrijfsnaam = model.Bedrijfsnaam,
                Omschrijving = model.Omschrijving,
                PhoneNumber = model.PhoneNumber,
                Locatie = model.Locatie,
                LinkNaarBedrijf = model.LinkNaarBedrijf,
                Email = model.Email,
                Rol = rol,
                /*EmailConfirmed = true*/
            };

            var result = await _userManager.CreateAsync(bedrijf, model.Wachtwoord);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "Wachtwoord is verkeerd" });
            }

            await _userManager.AddToRoleAsync(bedrijf, rolNaam);
            return new OkObjectResult(new Response { Status = "Success", Message = "User created successfully!" });
        }
        public async Task<IActionResult> RegistreerErvaringsdeskundige(RegistrerenErvaringdeskundige model)
        {

            var userExists = await _userManager.FindByEmailAsync(model.Email);
            var rol = await RolMaak("Ervaringsdeskundige");
            var Hulpmiddelen = _context.Hulpmiddelen.Where(a => model.Hulpmiddelen.Select(aa => aa.Id).Contains(a.Id)).ToList();
            var Aandoeningen = _context.Aandoeningen.Where(a => model.Aandoeningen.Select(aa => aa.Id).Contains(a.Id)).ToList();
            var TypeOnderzoeken = _context.TypeOnderzoeken.Where(t => model.TypeOnderzoeken.Select(to => to.Id).Contains(t.Id)).ToList();

            Voogd Voogd = null;

            if (userExists != null)
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "Email bestaat al!" });

            if (model.Minderjarig)
            {
                Voogd = await _context.Voogden.Where(v => v.Email == model.VoogdEmail).FirstOrDefaultAsync();
                if (Voogd == null)
                {
                    Voogd = new()
                    {
                        Voornaam = model.VoogdVoornaam,
                        Achternaam = model.VoogdAchternaam,
                        Email = model.VoogdEmail,
                        Telefoonnummer = model.VoogdTelefoonnummer
                    };

                    await _context.Voogden.AddAsync(Voogd);
                    await _context.SaveChangesAsync();
                }
            }
            Ervaringsdeskundige ervaringsdeskundige = new()
            {
                Voornaam = model.Voornaam,
                Achternaam = model.Achternaam,
                Postcode = model.Postcode,
                Minderjarig = model.Minderjarig,
                PhoneNumber = model.Telefoonnummer,
                Hulpmiddelen = Hulpmiddelen,
                Aandoeningen = Aandoeningen,
                VoorkeurBenadering = model.VoorkeurBenadering,
                TypeOnderzoeken = TypeOnderzoeken,
                UserName = model.Email,
                Commerciële = model.Commerciële,
                Email = model.Email,
                Rol = rol,
                Voogd = Voogd
            };

            var result = await _userManager.CreateAsync(ervaringsdeskundige, model.Wachtwoord);
            if (!result.Succeeded)
            {
                return new BadRequestObjectResult(new Response { Status = "Error", Message = "Wachtwoord is verkeerd" });
            }

            //email verzend stuk kan ook misschien een methode worden?
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(ervaringsdeskundige);
            var urlHelper = _urlHelperFactory.GetUrlHelper(_actionContextAccessor.ActionContext);
            var link = urlHelper.Action(nameof(VerifieerEmail), "Authenticatie", new { token, email = ervaringsdeskundige.Email }, _httpContextAccessor.HttpContext.Request.Scheme);
            await _emailSender.SendEmailAsync(ervaringsdeskundige.Email, "Verifieer email - Accessibility", link);
            await _userManager.AddToRoleAsync(ervaringsdeskundige, "Ervaringsdeskundige");
            /*	await _userManager.AddToRoleAsync(ervaringsdeskundige, "Ervaringsdeskundige");*/
            return new OkObjectResult(new Response { Status = "Success", Message = "Er is een verificatie email verstuurd naar: " + ervaringsdeskundige.Email /*+ "! LINK:" + link*/ });
        }
        public async Task<IActionResult> VerwijderGebruiker(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new NotFoundObjectResult(new Response() { Status = "Error", Message = "Gebruiker niet gevonden" });
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return new OkObjectResult(new Response() {Status = "Success", Message = "Gebruiker is verwijdert" });
            }
            else
            {
                return new BadRequestObjectResult(new Response() { Status = "Error", Message = "Actie gefaald" });
            }
        }
    }
}
