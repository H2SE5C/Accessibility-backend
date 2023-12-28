using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accessibility_backend.Migrations
{
    public partial class gebruikerTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Bericht_Gebruiker_OntvangerId",
                table: "Bericht");

            migrationBuilder.DropForeignKey(
                name: "FK_Bericht_Gebruiker_VerzenderId",
                table: "Bericht");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_Bericht_OntvangerId",
                table: "Bericht");

            migrationBuilder.DropIndex(
                name: "IX_Bericht_VerzenderId",
                table: "Bericht");

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
                name: "Email",
                table: "Gebruiker",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Gebruiker",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "AccessFailedCount",
                table: "Gebruiker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "Gebruiker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailConfirmed",
                table: "Gebruiker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "LockoutEnabled",
                table: "Gebruiker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LockoutEnd",
                table: "Gebruiker",
                type: "datetimeoffset",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedEmail",
                table: "Gebruiker",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedUserName",
                table: "Gebruiker",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PasswordHash",
                table: "Gebruiker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Gebruiker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "PhoneNumberConfirmed",
                table: "Gebruiker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SecurityStamp",
                table: "Gebruiker",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorEnabled",
                table: "Gebruiker",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Gebruiker",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Gebruiker",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Gebruiker",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Bericht_OntvangerId1",
                table: "Bericht",
                column: "OntvangerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Bericht_VerzenderId1",
                table: "Bericht",
                column: "VerzenderId1");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                name: "FK_Bericht_Gebruiker_OntvangerId1",
                table: "Bericht");

            migrationBuilder.DropForeignKey(
                name: "FK_Bericht_Gebruiker_VerzenderId1",
                table: "Bericht");

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "Gebruiker");

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "Gebruiker");

            migrationBuilder.DropIndex(
                name: "IX_Bericht_OntvangerId1",
                table: "Bericht");

            migrationBuilder.DropIndex(
                name: "IX_Bericht_VerzenderId1",
                table: "Bericht");

            migrationBuilder.DropColumn(
                name: "AccessFailedCount",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "EmailConfirmed",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "LockoutEnabled",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "LockoutEnd",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "NormalizedEmail",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "NormalizedUserName",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "PhoneNumberConfirmed",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "SecurityStamp",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "TwoFactorEnabled",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Gebruiker");

            migrationBuilder.DropColumn(
                name: "OntvangerId1",
                table: "Bericht");

            migrationBuilder.DropColumn(
                name: "VerzenderId1",
                table: "Bericht");

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

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Gebruiker",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Gebruiker",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)")
                .Annotation("SqlServer:Identity", "1, 1");

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

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bericht_OntvangerId",
                table: "Bericht",
                column: "OntvangerId");

            migrationBuilder.CreateIndex(
                name: "IX_Bericht_VerzenderId",
                table: "Bericht",
                column: "VerzenderId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

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
                name: "FK_Bericht_Gebruiker_OntvangerId",
                table: "Bericht",
                column: "OntvangerId",
                principalTable: "Gebruiker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Bericht_Gebruiker_VerzenderId",
                table: "Bericht",
                column: "VerzenderId",
                principalTable: "Gebruiker",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
