using Accessibility_backend.Modellen.Extra;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_app.Models
{
	[Table("Gebruiker")]
	public class Gebruiker : IdentityUser<int>
    {
		public int RolId { get; set; }
		public Rol Rol { get; set; }
		public DateTime LaatstIngelogd { get; set; } = DateTime.Now;
		public List<Bericht>? Berichten { get; set; } = new();

		/*public override string UserName
		{
			get => Email;
			set => Email = value;
		}*/
		/*		public bool VergelijkWachtwoord(string wachtwoord)
				{
					return false;
				}*/
	}
}



