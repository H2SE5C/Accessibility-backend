using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Log")]
	public class Log
    {
		public int Id { get; set; }
		public string Categorie { get; set; }
        public DateTime Tijdstempel { get; set; }
        public string IP_adres { get; set; }
        public string Beschrijving { get; set; }
    }
}

