namespace Accessibility_backend
{
	public interface IEmailSender
	{
		Task SendEmailAsync (string email, string subject, string message);
	}
}
