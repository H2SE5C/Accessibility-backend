
using Microsoft.Data.SqlClient;
using System.Net;
using System.Net.Mail;

namespace Accessibility_backend
{
	public class EmailSender : IEmailSender
	{
		public Task SendEmailAsync(string email, string subject, string message)
		{
			var mail = "noreply.sender.accessibility@gmail.com";
			var pw = "ryinqyakdclfypnh";

			var client = new SmtpClient("smtp.gmail.com", 587) { 
				EnableSsl = true,
				Credentials = new NetworkCredential(mail, pw)
			};

			return client.SendMailAsync( new MailMessage (from: mail,
				to: email,
				subject,
				message
				));
		}
	}
}
