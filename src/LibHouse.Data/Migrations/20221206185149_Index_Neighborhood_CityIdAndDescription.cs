using Microsoft.EntityFrameworkCore.Migrations;

namespace LibHouse.Data.Migrations
{
    public partial class Index_Neighborhood_CityIdAndDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "idx_neighborhood_cityid_description",
                schema: "Business",
                table: "Neighborhood",
                columns: new[] { "CityId", "Description" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "idx_neighborhood_cityid_description",
                schema: "Business",
                table: "Neighborhood");
        }
    }
}