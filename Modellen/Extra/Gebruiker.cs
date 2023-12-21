using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Gebruiker")]
	public class Gebruiker
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Wachtwoord { get; set; }
		public string? Rol { get; set; } = null!;
		public DateTime LaatstIngelogd { get; set; } = DateTime.Now;
		public bool Geverifieerd { get; set; }
		public List<Bericht>? Berichten { get; set; } = new();

		/*		public bool VergelijkWachtwoord(string wachtwoord)
				{
					return false;
				}*/
	}
}



