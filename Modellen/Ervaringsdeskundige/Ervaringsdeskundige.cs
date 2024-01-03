using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Ervaringsdeskundige")]
	public class Ervaringsdeskundige : Gebruiker
    {
        //identity heeft standaard al telefoonnummer
		public string Voornaam { get; set; } = null!;
        public string Achternaam { get; set; } = null!;
        public string Postcode { get; set; } = null!;
        public bool Minderjarig { get; set; }
        public List<Hulpmiddel> Hulpmiddelen { get; set; } = new();
		public List<Aandoening> Aandoeningen { get; set; } = new();
		public List<Onderzoek> Onderzoeken { get; set; } = new();
        public string VoorkeurBenadering { get; set; } = null!;
        public List<TypeOnderzoek> TypeOnderzoeken { get; set; } = new();
        public bool Commercerciële { get; set; }
        public List<Beschikbaarheid>? Beschikbaarheisdata { get; set; } = new();
        public Voogd? Voogd { get; set; }

    }
}