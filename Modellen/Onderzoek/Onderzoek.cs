using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Onderzoek")]
	public class Onderzoek
    {
		public int Id { get; set; }
		public string Titel { get; set; }
        public string Omschrijving { get; set; }
        public Vragenlijst? Vragenlijst { get; set; }
        public string Beloning { get; set; }
        public string Status { get; set; }
        public Bedrijf Bedrijf { get; set; }
        public DateTime Datum { get; set; }
        public List<Ervaringsdeskundige> Ervaringsdeskundigen { get; set; }
        public List<Beperking> Beperkingen { get; set; }
        public TypeOnderzoek TypeOnderzoek { get; set; }
    }
}