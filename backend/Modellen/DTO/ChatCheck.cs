namespace Accessibility_backend.Modellen.DTO
{
	public class ChatCheck
	{
		public int Id { get; set; }
		public List<ChatCheckGebruiker> Gebruikers { get; set; }
	}
}
