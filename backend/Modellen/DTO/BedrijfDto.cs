using Accessibility_app.Models;
using Accessibility_backend.Modellen.Registreermodellen;

namespace Accessibility_backend.Modellen.DTO
{

    public class BedrijfDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Bedrijfsnaam { get; set; }
        public string Omschrijving { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string PhoneNumber { get; set; }
        public string Locatie { get; set; }
        public string LinkNaarBedrijf { get; set; }
        public List<OnderzoekDto> BedrijfsOnderzoekslijst { get; set; }

    }
}