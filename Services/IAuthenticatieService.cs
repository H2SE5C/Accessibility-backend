using Microsoft.AspNetCore.Mvc;

namespace Accessibility_backend.Services
{
    public interface IAuthenticatieService
    {
        Task<IActionResult> VerifieerEmail(string token, string email);
    }
}
