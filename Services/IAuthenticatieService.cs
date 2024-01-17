using Accessibility_backend.Modellen.Extra;
using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Mvc;

namespace Accessibility_backend.Services
{
    public interface IAuthenticatieService
    {
        Task<IActionResult> VerifieerEmail(string token, string email);
        Task<IActionResult> Login(LoginModel model);
        IActionResult LogUit();
        Task<IActionResult> Refresh();
        //registratie kan misschien in aparte service en token ook
        Task<IActionResult> RegistreerBeheerder(RegistrerenBasis model);
        Task<IActionResult> RegistreerDeveloper(RegistrerenBasis model);
        Task<IActionResult> RegistreerMedewerker(RegistrerenMedewerkers model);
        Task<IActionResult> RegistreerBedrijf(RegistrerenBedrijf model);
        Task<IActionResult> RegistreerErvaringsdeskundige(RegistrerenErvaringdeskundige model);
        Task<IActionResult> VerwijderGebruiker(string id);

    }
}
