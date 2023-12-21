using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accessibility_backend.Migrations
{
    public partial class foutweggehaald : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medewerker_Chat_ChatLijstId",
                table: "Medewerker");

            migrationBuilder.DropIndex(
                name: "IX_Medewerker_ChatLijstId",
                table: "Medewerker");

            migrationBuilder.DropColumn(
                name: "ChatLijstId",
                table: "Medewerker");

            migrationBuilder.AddColumn<int>(
                name: "MedewerkerId",
                table: "Chat",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chat_MedewerkerId",
                table: "Chat",
                column: "MedewerkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Chat_Medewerker_MedewerkerId",
                table: "Chat",
                column: "MedewerkerId",
                principalTable: "Medewerker",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chat_Medewerker_MedewerkerId",
                table: "Chat");

            migrationBuilder.DropIndex(
                name: "IX_Chat_MedewerkerId",
                table: "Chat");

            migrationBuilder.DropColumn(
                name: "MedewerkerId",
                table: "Chat");

            migrationBuilder.AddColumn<int>(
                name: "ChatLijstId",
                table: "Medewerker",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Medewerker_ChatLijstId",
                table: "Medewerker",
                column: "ChatLijstId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medewerker_Chat_ChatLijstId",
                table: "Medewerker",
                column: "ChatLijstId",
                principalTable: "Chat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
