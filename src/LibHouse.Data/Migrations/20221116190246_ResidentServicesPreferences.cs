using Microsoft.EntityFrameworkCore.Migrations;

namespace LibHouse.Data.Migrations
{
    public partial class ResidentServicesPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ServicesPreferences_Cleaning_HouseCleaningIsRequired",
                schema: "Business",
                table: "ResidentPreferences",
                type: "bit",
                nullable: true,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<bool>(
                name: "ServicesPreferences_Internet_InternetServiceIsRequired",
                schema: "Business",
                table: "ResidentPreferences",
                type: "bit",
                nullable: true,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<bool>(
                name: "ServicesPreferences_Television_CableTelevisionIsRequired",
                schema: "Business",
                table: "ResidentPreferences",
                type: "bit",
                nullable: true,
                defaultValueSql: "0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServicesPreferences_Cleaning_HouseCleaningIsRequired",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropColumn(
                name: "ServicesPreferences_Internet_InternetServiceIsRequired",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropColumn(
                name: "ServicesPreferences_Television_CableTelevisionIsRequired",
                schema: "Business",
                table: "ResidentPreferences");
        }
    }
}