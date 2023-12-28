using System.ComponentModel.DataAnnotations;

namespace Accessibility_backend.Modellen
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Voornaam is required")]
        public string? Voornaam { get; set; }

        [Required(ErrorMessage = "Voornaam is required")]
        public string? Achternaam { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Postcode is required")]
        public string? Postcode { get; set; }

        [Required(ErrorMessage = "Minderjarig is required")]
        public bool? Minderjarig { get; set; }

        [Required(ErrorMessage = "Telefoonnummer is required")]
        public string? Telefoonnummer { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
