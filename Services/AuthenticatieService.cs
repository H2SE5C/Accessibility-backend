using Accessibility_app.Data;
using Accessibility_app.Models;
using Accessibility_backend.Modellen.Extra;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
        
        public AuthenticatieService(
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
        public async Task<IActionResult> VerifieerEmail(string token, string email)
        {
            var gebruiker = await _userManager.FindByEmailAsync(email);
            if (gebruiker == null) {

                return new BadRequestObjectResult(new Response{ Status = "Error", Message = "Geen gebruiker gevonden" });
            }
            await _userManager.ConfirmEmailAsync(gebruiker, token);
            return new OkObjectResult("Geverifieerd! U kan dit venster sluiten.");
        }
    }
}
