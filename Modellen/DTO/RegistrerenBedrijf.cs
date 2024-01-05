using System.ComponentModel.DataAnnotations;

namespace Accessibility_backend.Modellen.Registreermodellen
{
    public class RegistrerenBedrijf
    {

        [EmailAddress]
        [Required(ErrorMessage = "Email is vereist")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Wachtwoord is vereist")]
        public string? Wachtwoord{ get; set; }

        [Required(ErrorMessage = "Bedrijfsnaam is vereist")]
        public string? Bedrijfsnaam { get; set; }

        [Required(ErrorMessage = "Omschrijving is vereist")]
        public string? Omschrijving { get; set; }

        [Required(ErrorMessage = "Locatie is vereist")]
        public string? Locatie { get; set; }

        [Required(ErrorMessage = "LinkNaarBedrijf is vereist")]
        public string? LinkNaarBedrijf { get; set; }
    }
}
