using Accessibility_app.Models;
using System.ComponentModel.DataAnnotations;

namespace Accessibility_backend.Modellen.Registreermodellen
{
	public class VragenlijstForm
	{
        [Required(ErrorMessage = "Title is required")]
        public string? Naam { get; set; }
    }
}
