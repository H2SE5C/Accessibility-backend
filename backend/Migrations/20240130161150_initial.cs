using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accessibility_backend.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Beperking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beperking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hulpmiddel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hulpmiddel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Log",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Categorie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tijdstempel = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IP_adres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Beschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Log", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeOnderzoek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeOnderzoek", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Voogd",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Achternaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefoonnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voogd", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vragenlijst",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vragenlijst", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaatstIngelogd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RolId = table.Column<int>(type: "int", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetRoles_RolId",
                        column: x => x.RolId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Aandoening",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BeperkingId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aandoening", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aandoening_Beperking_BeperkingId",
                        column: x => x.BeperkingId,
                        principalTable: "Beperking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vraag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VraagTekst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VragenlijstId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vraag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vraag_Vragenlijst_VragenlijstId",
                        column: x => x.VragenlijstId,
                        principalTable: "Vragenlijst",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bedrijf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Bedrijfsnaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Locatie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkNaarBedrijf = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bedrijf", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bedrijf_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ervaringsdeskundige",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Voornaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Achternaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Minderjarig = table.Column<bool>(type: "bit", nullable: false),
                    VoorkeurBenadering = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commerciële = table.Column<bool>(type: "bit", nullable: false),
                    VoogdId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ervaringsdeskundige", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ervaringsdeskundige_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ervaringsdeskundige_Voogd_VoogdId",
                        column: x => x.VoogdId,
                        principalTable: "Voogd",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Medewerker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medewerker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medewerker_AspNetUsers_Id",
                        column: x => x.Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Antwoord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VraagId = table.Column<int>(type: "int", nullable: false),
                    GebruikerId = table.Column<int>(type: "int", nullable: false),
                    Uitkomst = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antwoord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Antwoord_AspNetUsers_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Antwoord_Vraag_VraagId",
                        column: x => x.VraagId,
                        principalTable: "Vraag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Onderzoek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VragenlijstId = table.Column<int>(type: "int", nullable: true),
                    Beloning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BedrijfId = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeOnderzoekId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Onderzoek", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Onderzoek_Bedrijf_BedrijfId",
                        column: x => x.BedrijfId,
                        principalTable: "Bedrijf",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Onderzoek_TypeOnderzoek_TypeOnderzoekId",
                        column: x => x.TypeOnderzoekId,
                        principalTable: "TypeOnderzoek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Onderzoek_Vragenlijst_VragenlijstId",
                        column: x => x.VragenlijstId,
                        principalTable: "Vragenlijst",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AandoeningErvaringsdeskundige",
                columns: table => new
                {
                    AandoeningenId = table.Column<int>(type: "int", nullable: false),
                    ErvaringsdeskundigenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AandoeningErvaringsdeskundige", x => new { x.AandoeningenId, x.ErvaringsdeskundigenId });
                    table.ForeignKey(
                        name: "FK_AandoeningErvaringsdeskundige_Aandoening_AandoeningenId",
                        column: x => x.AandoeningenId,
                        principalTable: "Aandoening",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AandoeningErvaringsdeskundige_Ervaringsdeskundige_ErvaringsdeskundigenId",
                        column: x => x.ErvaringsdeskundigenId,
                        principalTable: "Ervaringsdeskundige",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Beschikbaarheid",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dag = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Begintijd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Eindtijd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ErvaringsdeskundigeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beschikbaarheid", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Beschikbaarheid_Ervaringsdeskundige_ErvaringsdeskundigeId",
                        column: x => x.ErvaringsdeskundigeId,
                        principalTable: "Ervaringsdeskundige",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErvaringsdeskundigeHulpmiddel",
                columns: table => new
                {
                    ErvaringsdeskundigenId = table.Column<int>(type: "int", nullable: false),
                    HulpmiddelenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErvaringsdeskundigeHulpmiddel", x => new { x.ErvaringsdeskundigenId, x.HulpmiddelenId });
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeHulpmiddel_Ervaringsdeskundige_ErvaringsdeskundigenId",
                        column: x => x.ErvaringsdeskundigenId,
                        principalTable: "Ervaringsdeskundige",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeHulpmiddel_Hulpmiddel_HulpmiddelenId",
                        column: x => x.HulpmiddelenId,
                        principalTable: "Hulpmiddel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ErvaringsdeskundigeTypeOnderzoek",
                columns: table => new
                {
                    ErvaringsdeskundigenId = table.Column<int>(type: "int", nullable: false),
                    TypeOnderzoekenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErvaringsdeskundigeTypeOnderzoek", x => new { x.ErvaringsdeskundigenId, x.TypeOnderzoekenId });
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeTypeOnderzoek_Ervaringsdeskundige_ErvaringsdeskundigenId",
                        column: x => x.ErvaringsdeskundigenId,
                        principalTable: "Ervaringsdeskundige",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeTypeOnderzoek_TypeOnderzoek_TypeOnderzoekenId",
                        column: x => x.TypeOnderzoekenId,
                        principalTable: "TypeOnderzoek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BeperkingOnderzoek",
                columns: table => new
                {
                    BeperkingenId = table.Column<int>(type: "int", nullable: false),
                    OnderzoekenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeperkingOnderzoek", x => new { x.BeperkingenId, x.OnderzoekenId });
                    table.ForeignKey(
                        name: "FK_BeperkingOnderzoek_Beperking_BeperkingenId",
                        column: x => x.BeperkingenId,
                        principalTable: "Beperking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeperkingOnderzoek_Onderzoek_OnderzoekenId",
                        column: x => x.OnderzoekenId,
                        principalTable: "Onderzoek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Aanmaakdatum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OnderzoekId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chat", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chat_Onderzoek_OnderzoekId",
                        column: x => x.OnderzoekId,
                        principalTable: "Onderzoek",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ErvaringsdeskundigeOnderzoek",
                columns: table => new
                {
                    ErvaringsdeskundigenId = table.Column<int>(type: "int", nullable: false),
                    OnderzoekenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErvaringsdeskundigeOnderzoek", x => new { x.ErvaringsdeskundigenId, x.OnderzoekenId });
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeOnderzoek_Ervaringsdeskundige_ErvaringsdeskundigenId",
                        column: x => x.ErvaringsdeskundigenId,
                        principalTable: "Ervaringsdeskundige",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ErvaringsdeskundigeOnderzoek_Onderzoek_OnderzoekenId",
                        column: x => x.OnderzoekenId,
                        principalTable: "Onderzoek",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bericht",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tekst = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tijdstempel = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VerzenderId = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bericht", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bericht_AspNetUsers_VerzenderId",
                        column: x => x.VerzenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bericht_Chat_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChatGebruiker",
                columns: table => new
                {
                    ChatsId = table.Column<int>(type: "int", nullable: false),
                    GebruikersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatGebruiker", x => new { x.ChatsId, x.GebruikersId });
                    table.ForeignKey(
                        name: "FK_ChatGebruiker_AspNetUsers_GebruikersId",
                        column: x => x.GebruikersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatGebruiker_Chat_ChatsId",
                        column: x => x.ChatsId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Beperking",
                columns: new[] { "Id", "Naam" },
                values: new object[,]
                {
                    { 1, "Visueel" },
                    { 2, "Auditief" },
                    { 3, "Motorisch" },
                    { 4, "Cognitief" }
                });

            migrationBuilder.InsertData(
                table: "Hulpmiddel",
                columns: new[] { "Id", "Naam" },
                values: new object[,]
                {
                    { 1, "Schermlezers" },
                    { 2, "Brailleleesregels" },
                    { 3, "Contrast- en kleurinstellingen" },
                    { 4, "Aangepaste toetsenborden" }
                });

            migrationBuilder.InsertData(
                table: "TypeOnderzoek",
                columns: new[] { "Id", "Naam" },
                values: new object[,]
                {
                    { 1, "Vragenlijsten" },
                    { 2, "Onderzoek op locaties" },
                    { 3, "websites testen" }
                });

            migrationBuilder.InsertData(
                table: "Aandoening",
                columns: new[] { "Id", "BeperkingId", "Naam" },
                values: new object[,]
                {
                    { 1, 1, "Blindheid" },
                    { 2, 1, "Slechtziendheid" },
                    { 3, 1, "Kleurenblindheid" },
                    { 4, 2, "Doofheid" },
                    { 5, 2, "Slechthorendheid" },
                    { 6, 3, "Verlamming" },
                    { 7, 3, "Tremoren of beperkte motorische controle" },
                    { 8, 4, "ADHD" },
                    { 9, 4, "Dyslexie" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aandoening_BeperkingId",
                table: "Aandoening",
                column: "BeperkingId");

            migrationBuilder.CreateIndex(
                name: "IX_AandoeningErvaringsdeskundige_ErvaringsdeskundigenId",
                table: "AandoeningErvaringsdeskundige",
                column: "ErvaringsdeskundigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Antwoord_GebruikerId",
                table: "Antwoord",
                column: "GebruikerId");

            migrationBuilder.CreateIndex(
                name: "IX_Antwoord_VraagId",
                table: "Antwoord",
                column: "VraagId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RolId",
                table: "AspNetUsers",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BeperkingOnderzoek_OnderzoekenId",
                table: "BeperkingOnderzoek",
                column: "OnderzoekenId");

            migrationBuilder.CreateIndex(
                name: "IX_Bericht_ChatId",
                table: "Bericht",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Bericht_VerzenderId",
                table: "Bericht",
                column: "VerzenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Beschikbaarheid_ErvaringsdeskundigeId",
                table: "Beschikbaarheid",
                column: "ErvaringsdeskundigeId");

            migrationBuilder.CreateIndex(
                name: "IX_Chat_OnderzoekId",
                table: "Chat",
                column: "OnderzoekId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatGebruiker_GebruikersId",
                table: "ChatGebruiker",
                column: "GebruikersId");

            migrationBuilder.CreateIndex(
                name: "IX_Ervaringsdeskundige_VoogdId",
                table: "Ervaringsdeskundige",
                column: "VoogdId");

            migrationBuilder.CreateIndex(
                name: "IX_ErvaringsdeskundigeHulpmiddel_HulpmiddelenId",
                table: "ErvaringsdeskundigeHulpmiddel",
                column: "HulpmiddelenId");

            migrationBuilder.CreateIndex(
                name: "IX_ErvaringsdeskundigeOnderzoek_OnderzoekenId",
                table: "ErvaringsdeskundigeOnderzoek",
                column: "OnderzoekenId");

            migrationBuilder.CreateIndex(
                name: "IX_ErvaringsdeskundigeTypeOnderzoek_TypeOnderzoekenId",
                table: "ErvaringsdeskundigeTypeOnderzoek",
                column: "TypeOnderzoekenId");

            migrationBuilder.CreateIndex(
                name: "IX_Onderzoek_BedrijfId",
                table: "Onderzoek",
                column: "BedrijfId");

            migrationBuilder.CreateIndex(
                name: "IX_Onderzoek_TypeOnderzoekId",
                table: "Onderzoek",
                column: "TypeOnderzoekId");

            migrationBuilder.CreateIndex(
                name: "IX_Onderzoek_VragenlijstId",
                table: "Onderzoek",
                column: "VragenlijstId");

            migrationBuilder.CreateIndex(
                name: "IX_Vraag_VragenlijstId",
                table: "Vraag",
                column: "VragenlijstId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AandoeningErvaringsdeskundige");

            migrationBuilder.DropTable(
                name: "Antwoord");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "BeperkingOnderzoek");

            migrationBuilder.DropTable(
                name: "Bericht");

            migrationBuilder.DropTable(
                name: "Beschikbaarheid");

            migrationBuilder.DropTable(
                name: "ChatGebruiker");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeHulpmiddel");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeOnderzoek");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeTypeOnderzoek");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Medewerker");

            migrationBuilder.DropTable(
                name: "Aandoening");

            migrationBuilder.DropTable(
                name: "Vraag");

            migrationBuilder.DropTable(
                name: "Chat");

            migrationBuilder.DropTable(
                name: "Hulpmiddel");

            migrationBuilder.DropTable(
                name: "Ervaringsdeskundige");

            migrationBuilder.DropTable(
                name: "Beperking");

            migrationBuilder.DropTable(
                name: "Onderzoek");

            migrationBuilder.DropTable(
                name: "Voogd");

            migrationBuilder.DropTable(
                name: "Bedrijf");

            migrationBuilder.DropTable(
                name: "TypeOnderzoek");

            migrationBuilder.DropTable(
                name: "Vragenlijst");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");
        }
    }
}
