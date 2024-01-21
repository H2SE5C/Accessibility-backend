using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Mvc;

namespace Accessibility_backend
{
	public interface IEmailSender
	{
		Task<Response> SendEmailAsync (string email, string subject, string message);
	}
}
