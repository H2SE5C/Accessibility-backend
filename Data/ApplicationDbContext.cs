using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Accessibility_app.Models;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Accessibility_backend.Modellen.Extra;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Accessibility_app.Data;

public class ApplicationDbContext : IdentityDbContext<Gebruiker,Rol,int>
{
	public DbSet<Aandoening> Aandoeningen { get; set; }
    public DbSet<Rol> Rollen { get; set; }
    public DbSet<Antwoord> Antwoorden { get; set; }
	public DbSet<Bedrijf> Bedrijven { get; set; }
	public DbSet<Beperking> Beperkingen { get; set; }
	public DbSet<Bericht> Berichten { get; set; }
	public DbSet<Beschikbaarheid> Beschikbaarheden { get; set; }
	public DbSet<Chat> Chats { get; set; }  
	public DbSet<Ervaringsdeskundige> Ervaringsdeskundigen { get; set; }
	public DbSet<Hulpmiddel> Hulpmiddelen { get; set; }
	public DbSet<Log> Logs { get; set; }
	public DbSet<Gebruiker> Gebruikers { get; set; }
	public DbSet<Medewerker> Medewerkers { get; set; }
	public DbSet<Onderzoek> Onderzoeken { get; set; }
	public DbSet<TypeOnderzoek> TypeOnderzoeken { get; set; }
	public DbSet<Voogd> Voogden { get; set; }
	public DbSet<Vraag> Vragen { get; set; }
	public DbSet<Vragenlijst> Vragenlijsten { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);
		
		builder.Entity<Gebruiker>()
	   .HasMany(u => u.Berichten)
	   .WithOne(b => b.Verzender)
	   .HasForeignKey(b => b.VerzenderId)
	   .OnDelete(DeleteBehavior.Restrict);

		builder.Entity<Bericht>()
			.HasOne(b => b.Ontvanger)
			.WithMany()
			.HasForeignKey(b => b.OntvangerId)
			.OnDelete(DeleteBehavior.Restrict);

		builder.Entity<Beperking>().HasData(
			new { Id = 1, Naam = "Visueel" },
			new { Id = 2, Naam = "Auditief" },
			new { Id = 3, Naam = "Motorisch" },
			new { Id = 4, Naam = "Cognitief" }
			);

		builder.Entity<Aandoening>().HasData(
			new { Id = 1, Naam = "Blindheid", BeperkingId = 1 },
			new { Id = 2, Naam = "Slechtziendheid", BeperkingId = 1 },
			new { Id = 3, Naam = "Kleurenblindheid", BeperkingId = 1 },
			new { Id = 4, Naam = "Doofheid", BeperkingId = 2 },
			new { Id = 5, Naam = "Slechthorendheid", BeperkingId = 2 },
			new { Id = 6, Naam = "Verlamming", BeperkingId = 3 },
			new { Id = 7, Naam = "Tremoren of beperkte motorische controle", BeperkingId = 3 },
			new { Id = 8, Naam = "ADHD", BeperkingId = 4 },
			new { Id = 9, Naam = "Dyslexie", BeperkingId = 4 }
			);

		builder.Entity<TypeOnderzoek>().HasData(
			new { Id = 1, Naam = "Vragenlijsten" },
			new { Id = 2, Naam = "Onderzoek op locaties" },
			new { Id = 3, Naam = "websites testen" }
			);

		builder.Entity<Hulpmiddel>().HasData(
			new { Id = 1, Naam = "Schermlezers" },
			new { Id = 2, Naam = "Brailleleesregels" },
			new { Id = 3, Naam = "Contrast- en kleurinstellingen" },
			new { Id = 4, Naam = "Aangepaste toetsenborden" }
			);
		/*builder.Entity<Rol>().HasData(
			new { Id = 1, Naam = "Ervaringsdeskundige", Name = "Ervaringsdeskundige" },
            new { Id = 2, Naam = "Bedrijf", Name = "Bedrijf" },
            new { Id = 3, Naam = "Medewerker", Name = "Medewerker" },
            new { Id = 4, Naam = "Beheerder", Name = "Beheerder" }
            );
		builder.Entity<Ervaringsdeskundige>().HasData(
			new { 
				Id = 1,
				Rol = 1, 
				Voornaam = "Johnny", 
				Achternaam = "Bakker", 
				Postcode = "5847DE",
				Email = "ervaringsdeskundige@gmail.com",
				PhoneNumber = "0694343273",

				Minderjarig = false, 
				Hulpmiddelen = new Hulpmiddel(), 
				Aandoeningen = new List<int> {1,2}, 
				Onderzoeken = new List<int> { },
				VoorkeurBenadering = "Geen voorkeur", 
				TypeOnderzoeken = new List<int> {1,2}, 
				Commerciële = false, 
				Beschikbaarheidsdata = new Beschikbaarheid(),
				Voogd = new Voogd()
				}
			);*/
	}
}