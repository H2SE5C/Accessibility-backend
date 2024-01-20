using Accessibility_backend.Modellen.Registreermodellen;

namespace Accessibility_backend.Modellen.DTO
{
    public class OnderzoekenResponse
    {
        public List<OnderzoekDto> Goedgekeurd { get; set; }
        public List<OnderzoekDto> Aanvragen { get; set; }
    }
}
