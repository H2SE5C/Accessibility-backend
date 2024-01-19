using Accessibility_backend.Modellen.Registreermodellen;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace Accessibility_backend
{
	public class EmailSender : IEmailSender
	{
		public async Task<Response> SendEmailAsync(string receiver, string subject, string message)
		{
			try {
				var sender = "noreply.sender.accessibility@gmail.com";
				var pw = "ryinqyakdclfypnh";

				var client = new SmtpClient("smtp.gmail.com", 587)
				{
					EnableSsl = true,
					Credentials = new NetworkCredential(sender, pw)
				};
				await client.SendMailAsync(new MailMessage(
					from: sender,
					to: receiver,
					subject,
					message
					));

				return new Response { Status = "Success", Message = "Er is een verificatie email verstuurd naar: " + receiver };
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error email verzenden: {ex.Message}");
				return new Response { Status = "Error", Message = "Er is iets misgegaan..." };
			}
		}
	}
}
