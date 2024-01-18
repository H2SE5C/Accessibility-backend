using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
    [Table("Bericht")]
    public class Bericht
    {
        public int Id { get; set; }
        public string Tekst { get; set; }
        public DateTime Tijdstempel { get; set; } = DateTime.Now;
		public string VerzenderEmail { get; set; } = null!;
		public Chat Chat { get; set; } = null!;
    }
}