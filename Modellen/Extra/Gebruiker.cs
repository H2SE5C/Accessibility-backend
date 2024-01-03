using Accessibility_backend.Modellen.Extra;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Gebruiker")]
	public class Gebruiker : IdentityUser<int>
    {
		public string Wachtwoord { get; set; }
		public Rol Rol { get; set; }
		public DateTime LaatstIngelogd { get; set; } = DateTime.Now;
		public bool Geverifieerd { get; set; }
		public List<Bericht>? Berichten { get; set; } = new();

		/*		public bool VergelijkWachtwoord(string wachtwoord)
				{
					return false;
				}*/
	}
}



