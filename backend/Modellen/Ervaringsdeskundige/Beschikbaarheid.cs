using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Beschikbaarheid")]
	public class Beschikbaarheid
    {
		public int Id { get; set; }
		public string Dag { get; set; }
		//time only werkt niet op sqlserver wel op sqlite
        public DateTime Begintijd { get; set; }
		public DateTime Eindtijd { get; set; }
		public Ervaringsdeskundige Ervaringsdeskundige { get; set; } = null!;
    }
}

