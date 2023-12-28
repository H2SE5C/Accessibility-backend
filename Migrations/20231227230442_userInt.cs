using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accessibility_backend.Migrations
{
    public partial class userInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropIndex(
                name: "IX_Bericht_OntvangerId1",
                table: "Bericht");

            migrationBuilder.DropIndex(
                name: "IX_Bericht_VerzenderId1",
                table: "Bericht");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GebruikerDeveloper",
                table: "GebruikerDeveloper");

            migrationBuilder.DropColumn(
                name: "OntvangerId1",
                table: "Bericht");

            migrationBuilder.DropColumn(
                name: "VerzenderId1",
                table: "Bericht");

            migrationBuilder.RenameTable(
                name: "GebruikerDeveloper",
                newName: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "MedewerkerId",
                table: "Onderzoek",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BedrijfId",
                table: "Onderzoek",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Medewerker",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ErvaringsdeskundigenId",
                table: "ErvaringsdeskundigeTypeOnderzoek",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ErvaringsdeskundigenId",
                table: "ErvaringsdeskundigeOnderzoek",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ErvaringsdeskundigenId",
                table: "ErvaringsdeskundigeHulpmiddel",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Ervaringsdeskundige",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "MedewerkerId",
                table: "Chat",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ErvaringsdeskundigeId",
                table: "Beschikbaarheid",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ErvaringsdeskundigenId",
                table: "BeperkingErvaringsdeskundige",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Bedrijf",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "GebruikerId",
                table: "Antwoord",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "ErvaringsdeskundigenId",
                table: "AandoeningErvaringsdeskundige",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bericht_OntvangerId",
                table: "Bericht",
                column: "OntvangerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bericht_VerzenderId",
                table: "Bericht",
                column: "VerzenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Antwoord_AspNetUsers_GebruikerId",
                table: "Antwoord",
                column: "GebruikerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bedrijf_AspNetUsers_Id",
                table: "Bedrijf",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bericht_AspNetUsers_OntvangerId",
                table: "Bericht",
                column: "OntvangerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bericht_AspNetUsers_VerzenderId",
                table: "Bericht",
                column: "VerzenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Ervaringsdeskundige_AspNetUsers_Id",
                table: "Ervaringsdeskundige",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Medewerker_AspNetUsers_Id",
                table: "Medewerker",
                column: "Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Antwoord_AspNetUsers_GebruikerId",
                table: "Antwoord");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Bedrijf_AspNetUsers_Id",
                table: "Bedrijf");

            migrationBuilder.DropForeignKey(
                name: "FK_Bericht_AspNetUsers_OntvangerId",
                table: "Bericht");

            migrationBuilder.DropForeignKey(
                name: "FK_Bericht_AspNetUsers_VerzenderId",
                table: "Bericht");

            migrationBuilder.DropForeignKey(
                name: "FK_Ervaringsdeskundige_AspNetUsers_Id",
                table: "Ervaringsdeskundige");

            migrationBuilder.DropForeignKey(
                name: "FK_Medewerker_AspNetUsers_Id",
                table: "Medewerker");

            migrationBuilder.DropIndex(
                name: "IX_Bericht_OntvangerId",
                table: "Bericht");

            migrationBuilder.DropIndex(
                name: "IX_Bericht_VerzenderId",
                table: "Bericht");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "GebruikerDeveloper");

            migrationBuilder.AlterColumn<string>(
                name: "MedewerkerId",
                table: "Onderzoek",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BedrijfId",
                table: "Onderzoek",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Medewerker",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ErvaringsdeskundigenId",
                table: "ErvaringsdeskundigeTypeOnderzoek",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ErvaringsdeskundigenId",
                table: "ErvaringsdeskundigeOnderzoek",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ErvaringsdeskundigenId",
                table: "ErvaringsdeskundigeHulpmiddel",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Ervaringsdeskundige",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "MedewerkerId",
                table: "Chat",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ErvaringsdeskundigeId",
                table: "Beschikbaarheid",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "OntvangerId1",
                table: "Bericht",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VerzenderId1",
                table: "Bericht",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ErvaringsdeskundigenId",
                table: "BeperkingErvaringsdeskundige",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Bedrijf",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AspNetUserClaims",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetRoles",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "RoleId",
                table: "AspNetRoleClaims",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "GebruikerId",
                table: "Antwoord",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ErvaringsdeskundigenId",
                table: "AandoeningErvaringsdeskundige",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "GebruikerDeveloper",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GebruikerDeveloper",
                table: "GebruikerDeveloper",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bericht_OntvangerId1",
                table: "Bericht",
                column: "OntvangerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Bericht_VerzenderId1",
                table: "Bericht",
                column: "VerzenderId1");

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
    }
}
