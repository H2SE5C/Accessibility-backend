using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Aandoening")]
	public class Aandoening
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public List<Ervaringsdeskundige>? Ervaringsdeskundigen { get; set; } = new();
        public int BeperkingId { get; set; }
        public Beperking? Beperking { get; set; }
    }
}

