using System.ComponentModel.DataAnnotations;

namespace Accessibility_backend.Modellen
{
    public class registerDeveloper
    {

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Wachtwoord{ get; set; }
    }
}
