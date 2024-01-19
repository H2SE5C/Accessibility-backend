using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Antwoord")]
	public class Antwoord
    {
		public int Id { get; set; }
		public Vraag Vraag { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public string Uitkomst { get; set; }
    }
}