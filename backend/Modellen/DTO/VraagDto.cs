using Accessibility_app.Models;

namespace Accessibility_backend.Modellen.Registreermodellen
{
	public class VraagDto
	{
        public int Id { get; set; }
        public string VraagTekst { get; set; }
        public int? VragenlijstId { get; set; }
    }
}
