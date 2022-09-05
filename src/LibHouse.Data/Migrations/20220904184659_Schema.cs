using Microsoft.EntityFrameworkCore.Migrations;

namespace LibHouse.Data.Migrations
{
    public partial class Schema : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Business");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "Business");

            migrationBuilder.RenameTable(
                name: "Residents",
                newName: "Residents",
                newSchema: "Business");

            migrationBuilder.RenameTable(
                name: "Owners",
                newName: "Owners",
                newSchema: "Business");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Business",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Residents",
                schema: "Business",
                newName: "Residents");

            migrationBuilder.RenameTable(
                name: "Owners",
                schema: "Business",
                newName: "Owners");
        }
    }
}