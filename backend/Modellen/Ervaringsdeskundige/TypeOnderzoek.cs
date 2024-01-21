using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("TypeOnderzoek")]
	public class TypeOnderzoek
    {
		public int Id { get; set; }
		public string Naam { get; set; }
		public List<Ervaringsdeskundige>? Ervaringsdeskundigen { get; set; } = new();
	}
}

