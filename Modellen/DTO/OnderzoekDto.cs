using Accessibility_app.Models;

namespace Accessibility_backend.Modellen.Registreermodellen
{
	public class OnderzoekDto
	{
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Omschrijving { get; set; }
        public int? Vragenlijst { get; set; }
        public string Beloning { get; set; }
        public string Status { get; set; }
        public String Bedrijf { get; set; }
        public DateTime Datum { get; set; }
        public List<deskundigeEmailDto> Ervaringsdeskundigen { get; set; } = new();
        public List<BeperkingDto> Beperkingen { get; set; }
        public String TypeOnderzoek { get; set; }
    }
}
