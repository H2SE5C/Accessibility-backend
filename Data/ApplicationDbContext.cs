using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Accessibility_app.Models;
using System.Reflection.Emit;
using System.Reflection.Metadata;

namespace Accessibility_app.Data;

public class ApplicationDbContext : DbContext
{
	public DbSet<Aandoening> Aandoeningen { get; set; }
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
	public ApplicationDbContext(DbContextOptions options)
		: base(options){}

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

		//TPT inheritance?
		builder.Entity<Medewerker>().ToTable("Medewerker");
		builder.Entity<Bedrijf>().ToTable("Bedrijf");
		builder.Entity<Ervaringsdeskundige>().ToTable("Ervaringsdeskundige");
		builder.Entity<Gebruiker>().ToTable("Gebruiker");
	}
}

