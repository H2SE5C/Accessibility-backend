using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_backend.Modellen.Extra
{
	[Table("Rol")]
	public class Rol : IdentityRole<int>
    {
        public string Naam { get; set; }
    }
}
