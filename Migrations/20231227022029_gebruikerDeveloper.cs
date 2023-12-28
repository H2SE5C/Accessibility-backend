using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accessibility_backend.Migrations
{
    public partial class gebruikerDeveloper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Antwoord_Gebruiker_GebruikerId",
                table: "Antwoord");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_Gebruiker_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_Gebruiker_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Gebruiker_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_Gebruiker_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedrijf_Gebruiker_Id",
                table: "Bedrijf");

            migrationBuilder.DropForeignKey(
                name: "FK_Bericht_Gebruiker_OntvangerId1",
                table: "Bericht");

            migrationBuilder.DropForeignKey(
                name: "FK_Bericht_Gebruiker_VerzenderId1",
                table: "Bericht");

            migrationBuilder.DropForeignKey(
                name: "FK_Ervaringsdeskundige_Gebruiker_Id",
                table: "Ervaringsdeskundige");

            migrationBuilder.DropForeignKey(
                name: "FK_Medewerker_Gebruiker_Id",
                table: "Medewerker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Gebruiker",
                table: "Gebruiker");

            migrationBuilder.RenameTable(
                name: "Gebruiker",
                newName: "GebruikerDeveloper");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GebruikerDeveloper",
                table: "GebruikerDeveloper",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Antwoord_GebruikerDeveloper_GebruikerId",
                table: "Antwoord",
                column: "GebruikerId",
                principalTable: "GebruikerDeveloper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_GebruikerDeveloper_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "GebruikerDeveloper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_GebruikerDeveloper_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "GebruikerDeveloper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_GebruikerDeveloper_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "GebruikerDeveloper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_GebruikerDeveloper_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "GebruikerDeveloper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bedrijf_GebruikerDeveloper_Id",
                table: "Bedrijf",
                column: "Id",
                principalTable: "GebruikerDeveloper",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bericht_GebruikerDeveloper_OntvangerId1",
                table: "Bericht",
                column: "OntvangerId1",
                principalTable: "GebruikerDeveloper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bericht_GebruikerDeveloper_VerzenderId1",
                table: "Bericht",
                column: "VerzenderId1",
                principalTable: "GebruikerDeveloper",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ervaringsdeskundige_GebruikerDeveloper_Id",
                table: "Ervaringsdeskundige",
                column: "Id",
                principalTable: "GebruikerDeveloper",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medewerker_GebruikerDeveloper_Id",
                table: "Medewerker",
                column: "Id",
                principalTable: "GebruikerDeveloper",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Antwoord_GebruikerDeveloper_GebruikerId",
                table: "Antwoord");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_GebruikerDeveloper_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_GebruikerDeveloper_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_GebruikerDeveloper_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_GebruikerDeveloper_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedrijf_GebruikerDeveloper_Id",
                table: "Bedrijf");

            migrationBuilder.DropForeignKey(
                name: "FK_Bericht_GebruikerDeveloper_OntvangerId1",
                table: "Bericht");

            migrationBuilder.DropForeignKey(
                name: "FK_Bericht_GebruikerDeveloper_VerzenderId1",
                table: "Bericht");

            migrationBuilder.DropForeignKey(
                name: "FK_Ervaringsdeskundige_GebruikerDeveloper_Id",
                table: "Ervaringsdeskundige");

            migrationBuilder.DropForeignKey(
                name: "FK_Medewerker_GebruikerDeveloper_Id",
                table: "Medewerker");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GebruikerDeveloper",
                table: "GebruikerDeveloper");

            migrationBuilder.RenameTable(
                name: "GebruikerDeveloper",
                newName: "Gebruiker");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Gebruiker",
                table: "Gebruiker",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Antwoord_Gebruiker_GebruikerId",
                table: "Antwoord",
                column: "GebruikerId",
                principalTable: "Gebruiker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_Gebruiker_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "Gebruiker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_Gebruiker_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "Gebruiker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Gebruiker_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "Gebruiker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_Gebruiker_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "Gebruiker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bedrijf_Gebruiker_Id",
                table: "Bedrijf",
                column: "Id",
                principalTable: "Gebruiker",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bericht_Gebruiker_OntvangerId1",
                table: "Bericht",
                column: "OntvangerId1",
                principalTable: "Gebruiker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bericht_Gebruiker_VerzenderId1",
                table: "Bericht",
                column: "VerzenderId1",
                principalTable: "Gebruiker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ervaringsdeskundige_Gebruiker_Id",
                table: "Ervaringsdeskundige",
                column: "Id",
                principalTable: "Gebruiker",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medewerker_Gebruiker_Id",
                table: "Medewerker",
                column: "Id",
                principalTable: "Gebruiker",
                principalColumn: "Id");
        }
    }
}
