using Microsoft.AspNetCore.Identity;

namespace Accessibility_backend.Modellen.Extra
{
    public class Rol: IdentityRole<int>
    {
        public int Id { get; set; }
        public String Naam { get; set; }

    }
}
