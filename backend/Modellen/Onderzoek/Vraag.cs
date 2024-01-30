using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Vraag")]
	public class Vraag
    {
        internal readonly int? VragenlijstId;

        public int Id { get; set; }
		public string VraagTekst { get; set; }
        public object Vragenlijst { get; internal set; }
    }
}