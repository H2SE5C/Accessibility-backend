using Accessibility_app.Models;

namespace Accessibility_backend.Modellen.DTO
{
    public class BedrijvenResponse
    {
        public List<Bedrijf> bedrijvenTrue { get; set; }
        public List<Bedrijf> bedrijvenFalse { get; set; }
    }
}
