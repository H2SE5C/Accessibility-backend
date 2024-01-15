using Accessibility_app.Models;

namespace Accessibility_backend.Modellen.Registreermodellen
{
	public class ErvaringsdeskundigeDto
	{
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public string Postcode { get; set; }
        public bool Minderjarig { get; set; }
        public List<HulpmiddelDto> Hulpmiddelen { get; set; }
        public List<AandoeningDto> Aandoeningen { get; set; }
        public List<OnderzoekDto> Onderzoeken { get; set; }
        public string VoorkeurBenadering { get; set; }
        public List<TypeOnderzoekDto> TypeOnderzoeken { get; set; }
        public bool Commercerciële { get; set; }
        public List<Beschikbaarheid>? Beschikbaarheisdata { get; set; }
        public Voogd? Voogd { get; set; }

    }
}
