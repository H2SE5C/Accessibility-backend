using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend.Modellen;
using Accessibility_backend.Modellen.Extra;
using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Response = Accessibility_backend.Modellen.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Accessibility_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatieController : ControllerBase
    {
        private readonly UserManager<Gebruiker> _userManager;
        private readonly RoleManager<Rol> _roleManager;
        private readonly IConfiguration _configuration;
		private readonly ApplicationDbContext _context;

		public AuthenticatieController(
            UserManager<Gebruiker> userManager,
            RoleManager<Rol> roleManager,
            IConfiguration configuration, ApplicationDbContext applicationDbContext)
        {
			_context = applicationDbContext;
			_userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] RegisterDeveloper model)
        {
            var user = await _userManager.FindByNameAsync(model.Email);
            
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Wachtwoord))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new (ClaimTypes.Email, user.Email),
                    new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized();
        }

        [HttpPost]
        [Route("registreer-beheerder")]
        public async Task<IActionResult> Registreren([FromBody] RegisterDeveloper model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            Gebruiker user = new()
            {
                UserName = model.Email,
                Email = model.Email,
                RolId = 2,
            };
            var result = await _userManager.CreateAsync(user, model.Wachtwoord);
            if (!result.Succeeded)
            {
                var exceptionText = result.Errors.Aggregate("User Creation Failed - Identity Exception. Errors were: \n\r\n\r", (current, error) => current + (" - " + error + "\n\r"));
                throw new Exception(exceptionText);
                /*return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });*/
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

       /* [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User already exists!" });

            IdentityUser user = new()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            if (!await _roleManager.RoleExistsAsync(UserRoles.Admin))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            if (await _roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
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
    }
}
