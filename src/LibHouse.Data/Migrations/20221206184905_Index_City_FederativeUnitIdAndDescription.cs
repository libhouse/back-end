using Microsoft.EntityFrameworkCore.Migrations;

namespace LibHouse.Data.Migrations
{
    public partial class Index_City_FederativeUnitIdAndDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "idx_city_federativeunitid_description",
                schema: "Business",
                table: "City",
                columns: new[] { "FederativeUnitId", "Description" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_city_federativeunitid_description",
                schema: "Business",
                table: "City");
        }
    }
}