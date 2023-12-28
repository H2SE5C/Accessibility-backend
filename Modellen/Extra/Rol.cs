using Microsoft.AspNetCore.Identity;

namespace Accessibility_backend.Modellen.Extra
{
    public class Rol: IdentityRole<int>
    {
        public string Naam { get; set; }

    }
}
