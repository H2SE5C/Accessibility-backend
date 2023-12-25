using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Medewerker")]
	public class Medewerker : Gebruiker
    {
        public string Naam { get; set; }
        public List<Chat> ChatLijst { get; set; } = new();
        public List<Onderzoek> OnderzoekenLijst { get; set; } = new();

        public Medewerker() : base() {
            Rol = "Medewerker";
        }
    }
}