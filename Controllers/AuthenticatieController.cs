using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend;
using Accessibility_backend.Modellen.Extra;
using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Response = Accessibility_backend.Modellen.Registreermodellen.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

[Route("api/[controller]")]
[ApiController]
public class AuthenticatieController : ControllerBase
{
    private readonly UserManager<Gebruiker> _userManager;
    private readonly RoleManager<Rol> _roleManager;
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly IEmailSender _emailSender;


    public AuthenticatieController(
        UserManager<Gebruiker> userManager,
        RoleManager<Rol> roleManager,
        IConfiguration configuration, 
        ApplicationDbContext applicationDbContext,
        IEmailSender emailSender)
    {
        _context = applicationDbContext;
        _userManager = userManager;
        _roleManager = roleManager;
        _configuration = configuration;
        _emailSender = emailSender;
    }

    //dit kan ergens anders zodat bedrijf het ook kan gebruiken misschien? idk
    [HttpGet("/verifieer")]
    public async Task<IActionResult> VerifieerEmail(string token, string email)
    {
        var gebruiker = await _userManager.FindByEmailAsync(email);
        if (gebruiker == null)
            return BadRequest();

        await _userManager.ConfirmEmailAsync(gebruiker, token);
        return Ok("Geverifieerd! U kan dit venster sluiten.");
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Geen gebruiker gevonden" });
        };
        if (!user.EmailConfirmed)
        {
            return Unauthorized(new Response { Status = "Error", Message = "Verifieer email eerst!" });
        }
        if (!await _userManager.CheckPasswordAsync(user, model.Wachtwoord))
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "Wachtwoord fout" });
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

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo,
            roles = userRoles,
        });
        /* }
         return Unauthorized();*/
    }

    //registratie vereist een goede wachtwoord: 1 hoofdletter, cijfer en rare teken
    [HttpPost]

    [HttpPost("registreer-beheerder")]
    public async Task<IActionResult> RegistreerBeheerder([FromBody] RegistrerenBasis model)
    {
        var rolNaam = "Beheerder";
        await RegistreerGebruikerMetRol(model, rolNaam);
        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }

    [HttpPost("registreer-developer")]
    public async Task<IActionResult> RegistreerDeveloper([FromBody] RegistrerenBasis model)
    {
        var rolNaam = "Developer";
        await RegistreerGebruikerMetRol(model, rolNaam);
        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }


    [HttpPost("registreer-medewerker")]
    public async Task<IActionResult> RegistreerMedewerker([FromBody] RegistrerenMedewerkers model)
    {
        var rolNaam = "Medewerker";
        var userExists = await _userManager.FindByNameAsync(model.Email);
        if (userExists != null)
        {
            throw new Exception("User already exists!");
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
            var exceptionText = result.Errors.Aggregate("User Creation Failed - Identity Exception. Errors were: \n\r\n\r", (current, error) => current + (" - " + error + "\n\r"));
            throw new Exception(exceptionText);
        }

        await _userManager.AddToRoleAsync(medewerker, rolNaam);
        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }


    // POST api/Authenticatie/registreer-Bedrijf
    //voorbeeld:
    /*{
        "Bedrijfsnaam": "AH",
        "Omschrijving": "Awesome",
        "wachtwoord": "String123@",
        "email": "Killer@example.com",
        "Locatie": "Amsterdam 21",
        "Link": "https://www.ah.nl/?gad_source=1&gclid=CjwKCAiA7t6sBhAiEiwAsaieYoHEM-7ASRzYNpFhpGe3o3Q7bwtwR4_96ouq7Q_cLXJEli9IenU3EhoCB_MQAvD_BwE"

        }*/
    [HttpPost("registreer-bedrijf")]
    public async Task<IActionResult> RegistreerBedrijf([FromBody] RegistrerenBedrijf model)
    {
        var rolNaam = "Bedrijf";
        var userExists = await _userManager.FindByNameAsync(model.Email);
        if (userExists != null)
        {
            throw new Exception("User already exists!");
        }

        var rol = await RolMaak(rolNaam);

        Bedrijf bedrijf = new()
        {
            UserName = model.Email,
            Bedrijfsnaam = model.Bedrijfsnaam,
            Omschrijving = model.Omschrijving,
            Locatie = model.Locatie,
            LinkNaarBedrijf = model.LinkNaarBedrijf,
            Email = model.Email,
            Rol = rol,
            /*EmailConfirmed = true*/
        };

        var result = await _userManager.CreateAsync(bedrijf, model.Wachtwoord);
        if (!result.Succeeded)
        {
            var exceptionText = result.Errors.Aggregate("User Creation Failed - Identity Exception. Errors were: \n\r\n\r", (current, error) => current + (" - " + error + "\n\r"));
            throw new Exception(exceptionText);
        }

        await _userManager.AddToRoleAsync(bedrijf, rolNaam);
        return Ok(new Response { Status = "Success", Message = "User created successfully!" });
    }

    // post /api/Authenticatie/registreer-ervaringsdeskundige
    //voorbeeld:
    /*{
        "voornaam": "Dude",
        "achternaam": "Awesome",
        "wachtwoord": "String123@",
        "email": "Killer@example.com",
        "postcode": "2224GE",
        "minderjarig": false,
        "telefoonnummer": "0684406262",
        "aandoeningen" : [
        {
              "id": 2,
              "naam": "Slechtziendheid"
        },
        {
              "id": 4,
             "naam": "ADHD"
        }
        ],
        "typeOnderzoeken": [
        {
            "id": 1,
            "naam": "Vragenlijst"
        }
        ],
        "voorkeurBenadering": "geen voorkeur",
        "commerciële": false
        }*/
    [HttpPost("registreer-ervaringsdeskundige")]
    public async Task<IActionResult> RegistreerErvaringsdeskundige([FromBody] RegistrerenErvaringdeskundige model)
    {

        var userExists = await _userManager.FindByEmailAsync(model.Email);
        var rol = await RolMaak("Ervaringsdeskundige");
        var Hulpmiddelen = _context.Hulpmiddelen.Where(a => model.Hulpmiddelen.Select(aa => aa.Id).Contains(a.Id)).ToList();
        var Aandoeningen = _context.Aandoeningen.Where(a => model.Aandoeningen.Select(aa => aa.Id).Contains(a.Id)).ToList();
        var TypeOnderzoeken = _context.TypeOnderzoeken.Where(t => model.TypeOnderzoeken.Select(to => to.Id).Contains(t.Id)).ToList();

        Voogd Voogd = null;

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

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
            var exceptionText = result.Errors.Aggregate("User Creation Failed - Identity Exception. Errors were: \n\r\n\r", (current, error) => current + (" - " + error + "\n\r"));
            throw new Exception(exceptionText);
            /*return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });*/
        }

        //email verzend stuk kan ook misschien een methode worden?
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(ervaringsdeskundige);
        var link = Url.Action(nameof(VerifieerEmail), "Authenticatie", new { token, email = ervaringsdeskundige.Email }, Request.Scheme);
        await _emailSender.SendEmailAsync(ervaringsdeskundige.Email, "Verifieer email - Accessibility", link);
        await _userManager.AddToRoleAsync(ervaringsdeskundige, "Ervaringsdeskundige");
        /*	await _userManager.AddToRoleAsync(ervaringsdeskundige, "Ervaringsdeskundige");*/
        return Ok(new Response { Status = "Success", Message = "Er is een verificatie email verstuurd naar: " + ervaringsdeskundige.Email + "!" });
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
        throw new Exception("User already exists!");
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


    [HttpDelete("{id}")]
    public async Task<IActionResult> VerwijderGebruiker(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            return NotFound("User not found");
        }

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            return Ok("User deleted successfully");
        }
        else
        {
            return BadRequest("Failed to delete user");
        }
    }

    /* [HttpPost]
   [Route("register-admin")]
     public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
     {
         return Ok(new Response { Status = "Success", Message = "User created successfully!" });
     }
*/


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

    /* [HttpGet]
     [AllowAnonymous]
     public async Task<IActionResult> ConfirmEmail(string userId, string token)
     {
         if (userId == null || token == null) { 
             return NotFound();
         }
     }*/
}
