using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Chat")]
	public class Chat
    {
		public int Id { get; set; }
		public List<Bericht> Berichten { get; } = new();
        public DateTime Aanmaakdatum { get; set; } = DateTime.Now;
		public int? OnderzoekId { get; set; }
        public Onderzoek? Onderzoek { get; set; }
        public List<Gebruiker> Gebruikers { get; set; }
    }
}