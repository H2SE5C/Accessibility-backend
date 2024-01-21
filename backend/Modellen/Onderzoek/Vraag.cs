using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Vraag")]
	public class Vraag
    {
		public int Id { get; set; }
		public string VraagTekst { get; set; }
    }
}