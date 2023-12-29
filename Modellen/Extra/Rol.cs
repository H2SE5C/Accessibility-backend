using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Accessibility_backend.Modellen.Extra
{
	[Table("Medewerker")]
	public class Rol : IdentityRole<int>
    {
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key]
		public int Id {  get; set; }
        public string Naam { get; set; }

    }
}
