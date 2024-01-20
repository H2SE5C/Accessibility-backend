using Accessibility_app.Models;

namespace Accessibility_backend.Modellen.DTO
{
	public class ChatDto
	{
		public int Id { get; set; }
		public DateTime Aanmaakdatum { get; set; }
		public List<ChatGebruikerDto> Gebruikers { get; set; }
	}
}
