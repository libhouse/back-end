using Microsoft.EntityFrameworkCore.Migrations;

namespace LibHouse.Data.Migrations
{
    public partial class ResidentGeneralPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "GeneralPreferences_Animal_WantSpaceForAnimals",
                schema: "Business",
                table: "ResidentPreferences",
                type: "bit",
                nullable: true,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<bool>(
                name: "GeneralPreferences_Children_AcceptChildren",
                schema: "Business",
                table: "ResidentPreferences",
                type: "bit",
                nullable: true,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<bool>(
                name: "GeneralPreferences_Party_WantsToParty",
                schema: "Business",
                table: "ResidentPreferences",
                type: "bit",
                nullable: true,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<bool>(
                name: "GeneralPreferences_Roommate_AcceptsOnlyFemaleRoommates",
                schema: "Business",
                table: "ResidentPreferences",
                type: "bit",
                nullable: true,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<bool>(
                name: "GeneralPreferences_Roommate_AcceptsOnlyMaleRoommates",
                schema: "Business",
                table: "ResidentPreferences",
                type: "bit",
                nullable: true,
                defaultValueSql: "0");

            migrationBuilder.AddColumn<byte>(
                name: "GeneralPreferences_Roommate_MaximumNumberOfRoommatesDesired",
                schema: "Business",
                table: "ResidentPreferences",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "GeneralPreferences_Roommate_MinimumNumberOfRoommatesDesired",
                schema: "Business",
                table: "ResidentPreferences",
                type: "tinyint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "GeneralPreferences_Smokers_AcceptSmokers",
                schema: "Business",
                table: "ResidentPreferences",
                type: "bit",
                nullable: true,
                defaultValueSql: "0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeneralPreferences_Animal_WantSpaceForAnimals",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropColumn(
                name: "GeneralPreferences_Children_AcceptChildren",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropColumn(
                name: "GeneralPreferences_Party_WantsToParty",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropColumn(
                name: "GeneralPreferences_Roommate_AcceptsOnlyFemaleRoommates",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropColumn(
                name: "GeneralPreferences_Roommate_AcceptsOnlyMaleRoommates",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropColumn(
                name: "GeneralPreferences_Roommate_MaximumNumberOfRoommatesDesired",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropColumn(
                name: "GeneralPreferences_Roommate_MinimumNumberOfRoommatesDesired",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropColumn(
                name: "GeneralPreferences_Smokers_AcceptSmokers",
                schema: "Business",
                table: "ResidentPreferences");
        }
    }
}