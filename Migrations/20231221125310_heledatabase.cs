using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accessibility_backend.Migrations
{
    public partial class heledatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aandoening",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aandoening", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gebruiker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Wachtwoord = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LaatstIngelogd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Geverifieerd = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gebruiker", x => x.Id);
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
                name: "Bedrijf",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Locatie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LinkNaarBedrijf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bedrijfsnaam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bedrijf", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bedrijf_Gebruiker_Id",
                        column: x => x.Id,
                        principalTable: "Gebruiker",
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
                    Telefoonnummer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VoorkeurBenadering = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Commecerciële = table.Column<bool>(type: "bit", nullable: false),
                    VoogdId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ervaringsdeskundige", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ervaringsdeskundige_Gebruiker_Id",
                        column: x => x.Id,
                        principalTable: "Gebruiker",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ervaringsdeskundige_Voogd_VoogdId",
                        column: x => x.VoogdId,
                        principalTable: "Voogd",
                        principalColumn: "Id");
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
                        name: "FK_Antwoord_Gebruiker_GebruikerId",
                        column: x => x.GebruikerId,
                        principalTable: "Gebruiker",
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
                name: "Beperking",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OnderzoekId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Beperking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BeperkingErvaringsdeskundige",
                columns: table => new
                {
                    BeperkingenId = table.Column<int>(type: "int", nullable: false),
                    ErvaringsdeskundigenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BeperkingErvaringsdeskundige", x => new { x.BeperkingenId, x.ErvaringsdeskundigenId });
                    table.ForeignKey(
                        name: "FK_BeperkingErvaringsdeskundige_Beperking_BeperkingenId",
                        column: x => x.BeperkingenId,
                        principalTable: "Beperking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BeperkingErvaringsdeskundige_Ervaringsdeskundige_ErvaringsdeskundigenId",
                        column: x => x.ErvaringsdeskundigenId,
                        principalTable: "Ervaringsdeskundige",
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
                    OntvangerId = table.Column<int>(type: "int", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bericht", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bericht_Gebruiker_OntvangerId",
                        column: x => x.OntvangerId,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bericht_Gebruiker_VerzenderId",
                        column: x => x.VerzenderId,
                        principalTable: "Gebruiker",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                });

            migrationBuilder.CreateTable(
                name: "Medewerker",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Naam = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatLijstId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medewerker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medewerker_Chat_ChatLijstId",
                        column: x => x.ChatLijstId,
                        principalTable: "Chat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medewerker_Gebruiker_Id",
                        column: x => x.Id,
                        principalTable: "Gebruiker",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Onderzoek",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Omschrijving = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VragenlijstId = table.Column<int>(type: "int", nullable: false),
                    Beloning = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BedrijfId = table.Column<int>(type: "int", nullable: false),
                    Datum = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TypeOnderzoekId = table.Column<int>(type: "int", nullable: false),
                    MedewerkerId = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_Onderzoek_Medewerker_MedewerkerId",
                        column: x => x.MedewerkerId,
                        principalTable: "Medewerker",
                        principalColumn: "Id");
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Beperking_OnderzoekId",
                table: "Beperking",
                column: "OnderzoekId");

            migrationBuilder.CreateIndex(
                name: "IX_BeperkingErvaringsdeskundige_ErvaringsdeskundigenId",
                table: "BeperkingErvaringsdeskundige",
                column: "ErvaringsdeskundigenId");

            migrationBuilder.CreateIndex(
                name: "IX_Bericht_ChatId",
                table: "Bericht",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Bericht_OntvangerId",
                table: "Bericht",
                column: "OntvangerId");

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
                name: "IX_Medewerker_ChatLijstId",
                table: "Medewerker",
                column: "ChatLijstId");

            migrationBuilder.CreateIndex(
                name: "IX_Onderzoek_BedrijfId",
                table: "Onderzoek",
                column: "BedrijfId");

            migrationBuilder.CreateIndex(
                name: "IX_Onderzoek_MedewerkerId",
                table: "Onderzoek",
                column: "MedewerkerId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Beperking_Onderzoek_OnderzoekId",
                table: "Beperking",
                column: "OnderzoekId",
                principalTable: "Onderzoek",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bericht_Chat_ChatId",
                table: "Bericht",
                column: "ChatId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Onderzoek_OnderzoekId",
                table: "Chat",
                column: "OnderzoekId",
                principalTable: "Onderzoek",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bedrijf_Gebruiker_Id",
                table: "Bedrijf");

            migrationBuilder.DropForeignKey(
                name: "FK_Medewerker_Gebruiker_Id",
                table: "Medewerker");

            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Onderzoek_OnderzoekId",
                table: "Chat");

            migrationBuilder.DropTable(
                name: "AandoeningErvaringsdeskundige");

            migrationBuilder.DropTable(
                name: "Antwoord");

            migrationBuilder.DropTable(
                name: "BeperkingErvaringsdeskundige");

            migrationBuilder.DropTable(
                name: "Bericht");

            migrationBuilder.DropTable(
                name: "Beschikbaarheid");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeHulpmiddel");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeOnderzoek");

            migrationBuilder.DropTable(
                name: "ErvaringsdeskundigeTypeOnderzoek");

            migrationBuilder.DropTable(
                name: "Log");

            migrationBuilder.DropTable(
                name: "Aandoening");

            migrationBuilder.DropTable(
                name: "Vraag");

            migrationBuilder.DropTable(
                name: "Beperking");

            migrationBuilder.DropTable(
                name: "Hulpmiddel");

            migrationBuilder.DropTable(
                name: "Ervaringsdeskundige");

            migrationBuilder.DropTable(
                name: "Voogd");

            migrationBuilder.DropTable(
                name: "Gebruiker");

            migrationBuilder.DropTable(
                name: "Onderzoek");

            migrationBuilder.DropTable(
                name: "Bedrijf");

            migrationBuilder.DropTable(
                name: "Medewerker");

            migrationBuilder.DropTable(
                name: "TypeOnderzoek");

            migrationBuilder.DropTable(
                name: "Vragenlijst");

            migrationBuilder.DropTable(
                name: "Chat");
        }
    }
}
