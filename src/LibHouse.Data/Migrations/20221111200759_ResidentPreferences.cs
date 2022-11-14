using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibHouse.Data.Migrations
{
    public partial class ResidentPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ResidentPreferences",
                schema: "Business",
                columns: table => new
                {
                    ResidentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomPreferences_Dormitory_DormitoryType = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: true),
                    RoomPreferences_Dormitory_RequireFurnishedDormitory = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "0"),
                    RoomPreferences_Bathroom_BathroomType = table.Column<string>(type: "varchar(6)", maxLength: 6, nullable: true),
                    RoomPreferences_Garage_GarageIsRequired = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "0"),
                    RoomPreferences_Kitchen_StoveIsRequired = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "0"),
                    RoomPreferences_Kitchen_MicrowaveIsRequired = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "0"),
                    RoomPreferences_Kitchen_RefrigeratorIsRequired = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "0"),
                    RoomPreferences_Other_ServiceAreaIsRequired = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "0"),
                    RoomPreferences_Other_RecreationAreaIsRequired = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "0")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentPreferences", x => x.ResidentId);
                    table.ForeignKey(
                        name: "FK_ResidentPreferences_Residents_ResidentId",
                        column: x => x.ResidentId,
                        principalSchema: "Business",
                        principalTable: "Residents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "idx_residentpreferences_residentid",
                schema: "Business",
                table: "ResidentPreferences",
                column: "ResidentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResidentPreferences",
                schema: "Business");
        }
    }
}
