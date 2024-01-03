using System.ComponentModel.DataAnnotations;

namespace Accessibility_backend.Modellen
{
    public class registerDeveloper
    {

        [EmailAddress]
        [Required(ErrorMessage = "Email is vereist")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is vereist")]
        public string? Wachtwoord{ get; set; }
    }
}
