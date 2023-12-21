using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
    [Table("Bericht")]
    public class Bericht
    {
        public int Id { get; set; }
        public string Tekst { get; set; }
        public DateTime Tijdstempel { get; set; }
		public int VerzenderId { get; set; }
		public Gebruiker Verzender { get; set; } = null!;
		public int OntvangerId { get; set; }
		public Gebruiker Ontvanger { get; set; } = null!;
		public Chat Chat { get; set; } = null!;
    }
}