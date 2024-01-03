using Accessibility_app.Models;
using System.ComponentModel.DataAnnotations;

namespace Accessibility_backend.Modellen.Registreermodellen
{
    //model voor het registreren van ervaringsdeskundige
    //misschien list van hulpmiddel, aandoening etc vervangen met list<int>?
    public class RegisterModel
    {
        [Required(ErrorMessage = "Voornaam is required")]
        public string Voornaam { get; set; }

        [Required(ErrorMessage = "Achternaam is required")]
        public string Achternaam { get; set; }

        [Required(ErrorMessage = "Wachtwoord is required")]
        public string Wachtwoord { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Postcode is required")]
        public string Postcode { get; set; }

        [Required(ErrorMessage = "Minderjarig is required")]
        public bool Minderjarig { get; set; }

        [Required(ErrorMessage = "Telefoonnummer is required")]
        public string Telefoonnummer { get; set; }
        public List<Hulpmiddel> Hulpmiddelen { get; set; } = new List<Hulpmiddel>();
        public List<Aandoening> Aandoeningen { get; set; }
        public string VoorkeurBenadering { get; set; } = null!;
        public List<TypeOnderzoek> TypeOnderzoeken { get; set; } = new();
        public bool Commercerciële { get; set; }

        public string? VoogdVoornaam { get; set; }
        public string? VoogdAchternaam { get; set; }
        public string? VoogdTelefoonnummer { get; set; }

        [EmailAddress]
        public string? VoogdEmail { get; set; }
    }
}
