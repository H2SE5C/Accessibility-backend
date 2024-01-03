using System.ComponentModel.DataAnnotations;

namespace Accessibility_backend.Modellen.Registreermodellen
{
    public class RegisterDeveloper
    {

        [EmailAddress]
        [Required(ErrorMessage = "Email is vereist")]
        public string? Email { get; set; }


        [Required(ErrorMessage = "Wachtwoord is vereist")]
        public string? Wachtwoord{ get; set; }
    }
}
