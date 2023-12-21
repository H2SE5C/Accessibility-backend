using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Medewerker")]
	public class Medewerker : Gebruiker
    {
        public int Id { get; set; }
        public string Naam { get; set; }
        public List<Chat> ChatLijst { get; set; }
        public List<Onderzoek> OnderzoekenLijst { get; set; }
    }
}