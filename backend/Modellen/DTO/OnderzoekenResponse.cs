using Accessibility_backend.Modellen.Registreermodellen;

namespace Accessibility_backend.Modellen.DTO
{
    public class OnderzoekenResponse
    {
        public List<OnderzoekDto> onderzoekenEerste { get; set; }
        public List<OnderzoekDto> onderzoekenTweede { get; set; }
    }
}
