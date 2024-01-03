using System.ComponentModel.DataAnnotations;

namespace Accessibility_backend.Modellen.Registreermodellen
{
    public class RegisterDeveloper
    {

        [EmailAddress]
        [Required(ErrorMessage = "Email is vereist")]
        public string? Email { get; set; }

<<<<<<< HEAD:Modellen/registerDeveloper.cs
        [Required(ErrorMessage = "Password is vereist")]
        public string? Wachtwoord{ get; set; }
=======
        [Required(ErrorMessage = "Password is required")]
        public string? Wachtwoord { get; set; }
>>>>>>> main:Modellen/DTO/registerDeveloper.cs
    }
}
