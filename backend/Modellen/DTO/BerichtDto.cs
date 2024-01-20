namespace Accessibility_backend.Modellen.DTO
{
	public class BerichtDto
	{
		public int Id { get; set; }
		public string Tekst { get; set; }
		public string Tijdstempel { get; set; }
		public ChatGebruikerDto Verzender { get; set; }
	}
}
