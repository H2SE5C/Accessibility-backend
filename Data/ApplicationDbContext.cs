using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Accessibility_app.Models;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Accessibility_backend.Modellen.Extra;

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

		builder.Entity<Rol>().HasData(
			new Rol { Id = 1, Naam = "Developer" },
            new Rol { Id = 2, Naam = "Beheerder" },
            new Rol { Id = 3, Naam = "Medewerker" },
            new Rol { Id = 4, Naam = "Ervaringsdeskundige" },
            new Rol { Id = 5, Naam = "Bedrijf" }
			);

	}
}

