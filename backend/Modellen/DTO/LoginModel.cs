using System.ComponentModel.DataAnnotations;
namespace Accessibility_backend.Modellen.Registreermodellen
{
    public class LoginModel
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is required")]
        public string? Wachtwoord { get; set; }
    }
}
