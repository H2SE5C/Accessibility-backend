using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Vragenlijst")]
	public class Vragenlijst
    {
		public int Id { get; set; }
		public string Naam { get; set; }
        public List<Vraag> Vragen { get; set; }
    }
}