﻿// <auto-generated />
using System;
using Accessibility_app.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Accessibility_backend.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240103013143_databaseMake")]
    partial class databaseMake
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.23")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AandoeningErvaringsdeskundige", b =>
                {
                    b.Property<int>("AandoeningenId")
                        .HasColumnType("int");

                    b.Property<int>("ErvaringsdeskundigenId")
                        .HasColumnType("int");

                    b.HasKey("AandoeningenId", "ErvaringsdeskundigenId");

                    b.HasIndex("ErvaringsdeskundigenId");

                    b.ToTable("AandoeningErvaringsdeskundige");
                });

            modelBuilder.Entity("Accessibility_app.Models.Aandoening", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Aandoening");
                });

            modelBuilder.Entity("Accessibility_app.Models.Antwoord", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GebruikerId")
                        .HasColumnType("int");

                    b.Property<string>("Uitkomst")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VraagId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GebruikerId");

                    b.HasIndex("VraagId");

                    b.ToTable("Antwoord");
                });

            modelBuilder.Entity("Accessibility_app.Models.Beperking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OnderzoekId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OnderzoekId");

                    b.ToTable("Beperking");
                });

            modelBuilder.Entity("Accessibility_app.Models.Bericht", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<int>("OntvangerId")
                        .HasColumnType("int");

                    b.Property<string>("Tekst")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Tijdstempel")
                        .HasColumnType("datetime2");

                    b.Property<int>("VerzenderId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("OntvangerId");

                    b.HasIndex("VerzenderId");

                    b.ToTable("Bericht");
                });

            modelBuilder.Entity("Accessibility_app.Models.Beschikbaarheid", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Begintijd")
                        .HasColumnType("datetime2");

                    b.Property<string>("Dag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Eindtijd")
                        .HasColumnType("datetime2");

                    b.Property<int>("ErvaringsdeskundigeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ErvaringsdeskundigeId");

                    b.ToTable("Beschikbaarheid");
                });

            modelBuilder.Entity("Accessibility_app.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Aanmaakdatum")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MedewerkerId")
                        .HasColumnType("int");

                    b.Property<int?>("OnderzoekId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MedewerkerId");

                    b.HasIndex("OnderzoekId");

                    b.ToTable("Chat");
                });

            modelBuilder.Entity("Accessibility_app.Models.Gebruiker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("Geverifieerd")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LaatstIngelogd")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("RolId")
                        .HasColumnType("int");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("Wachtwoord")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.HasIndex("RolId");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Accessibility_app.Models.Hulpmiddel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hulpmiddel");
                });

            modelBuilder.Entity("Accessibility_app.Models.Log", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Beschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Categorie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IP_adres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Tijdstempel")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Log");
                });

            modelBuilder.Entity("Accessibility_app.Models.Onderzoek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("BedrijfId")
                        .HasColumnType("int");

                    b.Property<string>("Beloning")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Datum")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MedewerkerId")
                        .HasColumnType("int");

                    b.Property<string>("Omschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TypeOnderzoekId")
                        .HasColumnType("int");

                    b.Property<int>("VragenlijstId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BedrijfId");

                    b.HasIndex("MedewerkerId");

                    b.HasIndex("TypeOnderzoekId");

                    b.HasIndex("VragenlijstId");

                    b.ToTable("Onderzoek");
                });

            modelBuilder.Entity("Accessibility_app.Models.TypeOnderzoek", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TypeOnderzoek");
                });

            modelBuilder.Entity("Accessibility_app.Models.Voogd", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefoonnummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Voogd");
                });

            modelBuilder.Entity("Accessibility_app.Models.Vraag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("VraagTekst")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VragenlijstId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VragenlijstId");

                    b.ToTable("Vraag");
                });

            modelBuilder.Entity("Accessibility_app.Models.Vragenlijst", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Vragenlijst");
                });

            modelBuilder.Entity("Accessibility_backend.Modellen.Extra.Rol", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ConcurrencyStamp = "2cbffd56-3419-41f9-bb71-48a736245c86",
                            Naam = "Developer"
                        },
                        new
                        {
                            Id = 2,
                            ConcurrencyStamp = "c15bbd53-2969-422a-884b-4be14ef40c1c",
                            Naam = "Beheerder"
                        },
                        new
                        {
                            Id = 3,
                            ConcurrencyStamp = "2aa19b0e-4588-4f92-91dc-6604415ad461",
                            Naam = "Medewerker"
                        },
                        new
                        {
                            Id = 4,
                            ConcurrencyStamp = "ffd30102-a6ce-474e-b2ab-5453372069cc",
                            Naam = "Ervarindeskundigen"
                        },
                        new
                        {
                            Id = 5,
                            ConcurrencyStamp = "3bc0c626-82e6-4a6f-b5ae-9392d01f8c9f",
                            Naam = "Bedrijf"
                        });
                });

            modelBuilder.Entity("BeperkingErvaringsdeskundige", b =>
                {
                    b.Property<int>("BeperkingenId")
                        .HasColumnType("int");

                    b.Property<int>("ErvaringsdeskundigenId")
                        .HasColumnType("int");

                    b.HasKey("BeperkingenId", "ErvaringsdeskundigenId");

                    b.HasIndex("ErvaringsdeskundigenId");

                    b.ToTable("BeperkingErvaringsdeskundige");
                });

            modelBuilder.Entity("ErvaringsdeskundigeHulpmiddel", b =>
                {
                    b.Property<int>("ErvaringsdeskundigenId")
                        .HasColumnType("int");

                    b.Property<int>("HulpmiddelenId")
                        .HasColumnType("int");

                    b.HasKey("ErvaringsdeskundigenId", "HulpmiddelenId");

                    b.HasIndex("HulpmiddelenId");

                    b.ToTable("ErvaringsdeskundigeHulpmiddel");
                });

            modelBuilder.Entity("ErvaringsdeskundigeOnderzoek", b =>
                {
                    b.Property<int>("ErvaringsdeskundigenId")
                        .HasColumnType("int");

                    b.Property<int>("OnderzoekenId")
                        .HasColumnType("int");

                    b.HasKey("ErvaringsdeskundigenId", "OnderzoekenId");

                    b.HasIndex("OnderzoekenId");

                    b.ToTable("ErvaringsdeskundigeOnderzoek");
                });

            modelBuilder.Entity("ErvaringsdeskundigeTypeOnderzoek", b =>
                {
                    b.Property<int>("ErvaringsdeskundigenId")
                        .HasColumnType("int");

                    b.Property<int>("TypeOnderzoekenId")
                        .HasColumnType("int");

                    b.HasKey("ErvaringsdeskundigenId", "TypeOnderzoekenId");

                    b.HasIndex("TypeOnderzoekenId");

                    b.ToTable("ErvaringsdeskundigeTypeOnderzoek");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Accessibility_app.Models.Bedrijf", b =>
                {
                    b.HasBaseType("Accessibility_app.Models.Gebruiker");

                    b.Property<string>("Bedrijfsnaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LinkNaarBedrijf")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Locatie")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Omschrijving")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Bedrijf");
                });

            modelBuilder.Entity("Accessibility_app.Models.Ervaringsdeskundige", b =>
                {
                    b.HasBaseType("Accessibility_app.Models.Gebruiker");

                    b.Property<string>("Achternaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Commecerciële")
                        .HasColumnType("bit");

                    b.Property<bool>("Minderjarig")
                        .HasColumnType("bit");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefoonnummer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("VoogdId")
                        .HasColumnType("int");

                    b.Property<string>("VoorkeurBenadering")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Voornaam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasIndex("VoogdId");

                    b.ToTable("Ervaringsdeskundige");
                });

            modelBuilder.Entity("Accessibility_app.Models.Medewerker", b =>
                {
                    b.HasBaseType("Accessibility_app.Models.Gebruiker");

                    b.Property<string>("Naam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("Medewerker");
                });

            modelBuilder.Entity("AandoeningErvaringsdeskundige", b =>
                {
                    b.HasOne("Accessibility_app.Models.Aandoening", null)
                        .WithMany()
                        .HasForeignKey("AandoeningenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accessibility_app.Models.Ervaringsdeskundige", null)
                        .WithMany()
                        .HasForeignKey("ErvaringsdeskundigenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Accessibility_app.Models.Antwoord", b =>
                {
                    b.HasOne("Accessibility_app.Models.Gebruiker", "Gebruiker")
                        .WithMany()
                        .HasForeignKey("GebruikerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accessibility_app.Models.Vraag", "Vraag")
                        .WithMany()
                        .HasForeignKey("VraagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gebruiker");

                    b.Navigation("Vraag");
                });

            modelBuilder.Entity("Accessibility_app.Models.Beperking", b =>
                {
                    b.HasOne("Accessibility_app.Models.Onderzoek", null)
                        .WithMany("Beperkingen")
                        .HasForeignKey("OnderzoekId");
                });

            modelBuilder.Entity("Accessibility_app.Models.Bericht", b =>
                {
                    b.HasOne("Accessibility_app.Models.Chat", "Chat")
                        .WithMany("Berichten")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accessibility_app.Models.Gebruiker", "Ontvanger")
                        .WithMany()
                        .HasForeignKey("OntvangerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Accessibility_app.Models.Gebruiker", "Verzender")
                        .WithMany("Berichten")
                        .HasForeignKey("VerzenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("Ontvanger");

                    b.Navigation("Verzender");
                });

            modelBuilder.Entity("Accessibility_app.Models.Beschikbaarheid", b =>
                {
                    b.HasOne("Accessibility_app.Models.Ervaringsdeskundige", "Ervaringsdeskundige")
                        .WithMany("Beschikbaarheisdata")
                        .HasForeignKey("ErvaringsdeskundigeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ervaringsdeskundige");
                });

            modelBuilder.Entity("Accessibility_app.Models.Chat", b =>
                {
                    b.HasOne("Accessibility_app.Models.Medewerker", null)
                        .WithMany("ChatLijst")
                        .HasForeignKey("MedewerkerId");

                    b.HasOne("Accessibility_app.Models.Onderzoek", "Onderzoek")
                        .WithMany()
                        .HasForeignKey("OnderzoekId");

                    b.Navigation("Onderzoek");
                });

            modelBuilder.Entity("Accessibility_app.Models.Gebruiker", b =>
                {
                    b.HasOne("Accessibility_backend.Modellen.Extra.Rol", "Rol")
                        .WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rol");
                });

            modelBuilder.Entity("Accessibility_app.Models.Onderzoek", b =>
                {
                    b.HasOne("Accessibility_app.Models.Bedrijf", "Bedrijf")
                        .WithMany("BedrijfsOnderzoekslijst")
                        .HasForeignKey("BedrijfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accessibility_app.Models.Medewerker", null)
                        .WithMany("OnderzoekenLijst")
                        .HasForeignKey("MedewerkerId");

                    b.HasOne("Accessibility_app.Models.TypeOnderzoek", "TypeOnderzoek")
                        .WithMany()
                        .HasForeignKey("TypeOnderzoekId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accessibility_app.Models.Vragenlijst", "Vragenlijst")
                        .WithMany()
                        .HasForeignKey("VragenlijstId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bedrijf");

                    b.Navigation("TypeOnderzoek");

                    b.Navigation("Vragenlijst");
                });

            modelBuilder.Entity("Accessibility_app.Models.Vraag", b =>
                {
                    b.HasOne("Accessibility_app.Models.Vragenlijst", null)
                        .WithMany("Vragen")
                        .HasForeignKey("VragenlijstId");
                });

            modelBuilder.Entity("BeperkingErvaringsdeskundige", b =>
                {
                    b.HasOne("Accessibility_app.Models.Beperking", null)
                        .WithMany()
                        .HasForeignKey("BeperkingenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accessibility_app.Models.Ervaringsdeskundige", null)
                        .WithMany()
                        .HasForeignKey("ErvaringsdeskundigenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ErvaringsdeskundigeHulpmiddel", b =>
                {
                    b.HasOne("Accessibility_app.Models.Ervaringsdeskundige", null)
                        .WithMany()
                        .HasForeignKey("ErvaringsdeskundigenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accessibility_app.Models.Hulpmiddel", null)
                        .WithMany()
                        .HasForeignKey("HulpmiddelenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ErvaringsdeskundigeOnderzoek", b =>
                {
                    b.HasOne("Accessibility_app.Models.Ervaringsdeskundige", null)
                        .WithMany()
                        .HasForeignKey("ErvaringsdeskundigenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accessibility_app.Models.Onderzoek", null)
                        .WithMany()
                        .HasForeignKey("OnderzoekenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ErvaringsdeskundigeTypeOnderzoek", b =>
                {
                    b.HasOne("Accessibility_app.Models.Ervaringsdeskundige", null)
                        .WithMany()
                        .HasForeignKey("ErvaringsdeskundigenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accessibility_app.Models.TypeOnderzoek", null)
                        .WithMany()
                        .HasForeignKey("TypeOnderzoekenId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("Accessibility_backend.Modellen.Extra.Rol", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("Accessibility_app.Models.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("Accessibility_app.Models.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<int>", b =>
                {
                    b.HasOne("Accessibility_backend.Modellen.Extra.Rol", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Accessibility_app.Models.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<int>", b =>
                {
                    b.HasOne("Accessibility_app.Models.Gebruiker", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Accessibility_app.Models.Bedrijf", b =>
                {
                    b.HasOne("Accessibility_app.Models.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("Accessibility_app.Models.Bedrijf", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Accessibility_app.Models.Ervaringsdeskundige", b =>
                {
                    b.HasOne("Accessibility_app.Models.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("Accessibility_app.Models.Ervaringsdeskundige", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("Accessibility_app.Models.Voogd", "Voogd")
                        .WithMany()
                        .HasForeignKey("VoogdId");

                    b.Navigation("Voogd");
                });

            modelBuilder.Entity("Accessibility_app.Models.Medewerker", b =>
                {
                    b.HasOne("Accessibility_app.Models.Gebruiker", null)
                        .WithOne()
                        .HasForeignKey("Accessibility_app.Models.Medewerker", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Accessibility_app.Models.Chat", b =>
                {
                    b.Navigation("Berichten");
                });

            modelBuilder.Entity("Accessibility_app.Models.Gebruiker", b =>
                {
                    b.Navigation("Berichten");
                });

            modelBuilder.Entity("Accessibility_app.Models.Onderzoek", b =>
                {
                    b.Navigation("Beperkingen");
                });

            modelBuilder.Entity("Accessibility_app.Models.Vragenlijst", b =>
                {
                    b.Navigation("Vragen");
                });

            modelBuilder.Entity("Accessibility_app.Models.Bedrijf", b =>
                {
                    b.Navigation("BedrijfsOnderzoekslijst");
                });

            modelBuilder.Entity("Accessibility_app.Models.Ervaringsdeskundige", b =>
                {
                    b.Navigation("Beschikbaarheisdata");
                });

            modelBuilder.Entity("Accessibility_app.Models.Medewerker", b =>
                {
                    b.Navigation("ChatLijst");

                    b.Navigation("OnderzoekenLijst");
                });
#pragma warning restore 612, 618
        }
    }
}
