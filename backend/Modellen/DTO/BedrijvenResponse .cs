using Accessibility_app.Models;

namespace Accessibility_backend.Modellen.DTO
{
    public class BedrijvenResponse
    {
        public List<BedrijfDto> bedrijvenTrue { get; set; }
        public List<BedrijfDto> bedrijvenFalse { get; set; }
    }
}
