using Accessibility_app.Models;
using System.ComponentModel.DataAnnotations;

namespace Accessibility_backend.Modellen.Registreermodellen
{
	public class OnderzoekForm
	{
        [Required(ErrorMessage = "Title is required")]
        public string? Titel { get; set; }

        [Required(ErrorMessage = "Omschrijving is required")]
        public string? Omschrijving { get; set; }


        [Required(ErrorMessage = "Beloning is required")]
        public string? Beloning { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string? Status { get; set; }

        [Required(ErrorMessage = "Bedrijf is required")]
        public int? BedrijfId { get; set; }

        [Required(ErrorMessage = "Datum is required")]
        public DateTime? Datum { get; set; }

        [Required(ErrorMessage = "TypeOnderzoek is required")]
        public String? TypeOnderzoek { get; set; }

        [Required(ErrorMessage = "Beparking  is required")]
        public List<Beperking> Beperkingen { get; set; }



    }
}
