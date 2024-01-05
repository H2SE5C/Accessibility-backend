using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend.Modellen.Extra;
using Accessibility_backend.Modellen.Registreermodellen;
using Azure;
using Microsoft.AspNetCore.Authorization;
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
				userRol = userRoles,
			});
			/* }
			 return Unauthorized();*/
		}
        }

		//registratie vereist een goede wachtwoord: 1 hoofdletter, cijfer en rare teken
		[HttpPost]

		[HttpPost("registreer-beheerder")]
		public async Task<IActionResult> RegistreerBeheerder([FromBody] RegisterDeveloper model)
		{
			var roleName = "Beheerder";
			await RegisterUserWithRole(model, roleName);
			return Ok(new Response { Status = "Success", Message = "User created successfully!" });
		}

		[HttpPost("registreer-developer")]
		public async Task<IActionResult> RegistreerDeveloper([FromBody] RegisterDeveloper model)
		{
			var roleName = "Developer";
			await RegisterUserWithRole(model, roleName);
			return Ok(new Response { Status = "Success", Message = "User created successfully!" });
		}

		private async Task RegisterUserWithRole(RegisterDeveloper model, string roleName)
		{
			var userExists = await _userManager.FindByNameAsync(model.Email);
			if (userExists != null)
			{
				throw new Exception("User already exists!");
			}

			var roleExists = await _roleManager.RoleExistsAsync(roleName);
			var role = new Rol { Naam = roleName, Name = roleName };
			if (!roleExists)
			{
				var res = await _roleManager.CreateAsync(role);
				if (!res.Succeeded)
				{
					throw new Exception("Role creation failed!");
				}
			}

			var rol = await _context.Rollen.FirstOrDefaultAsync(r => r.Naam == roleName);
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

			await _userManager.AddToRoleAsync(user, roleName);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> VerwijderGebruiker(string id)
		{
			var user = await _userManager.FindByIdAsync(id);
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
		/* [HttpGet]
		 [AllowAnonymous]
		 public async Task<IActionResult> ConfirmEmail(string userId, string token)
		 {
			 if (userId == null || token == null) { 
				 return NotFound();
			 }
		 }*/
	}
}
}
