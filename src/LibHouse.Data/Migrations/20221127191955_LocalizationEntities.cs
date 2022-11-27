using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibHouse.Data.Migrations
{
    public partial class LocalizationEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FederativeUnit",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Abbreviation = table.Column<string>(type: "char(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FederativeUnit", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "City",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    FederativeUnitId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_FederativeUnit_FederativeUnitId",
                        column: x => x.FederativeUnitId,
                        principalSchema: "Business",
                        principalTable: "FederativeUnit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Neighborhood",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    CityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Neighborhood", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Neighborhood_City_CityId",
                        column: x => x.CityId,
                        principalSchema: "Business",
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                schema: "Business",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    Number = table.Column<short>(type: "smallint", nullable: true),
                    Complement = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    NeighborhoodId = table.Column<int>(type: "int", nullable: false),
                    PostalCode = table.Column<string>(type: "char(8)", maxLength: 8, nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Address_Neighborhood_NeighborhoodId",
                        column: x => x.NeighborhoodId,
                        principalSchema: "Business",
                        principalTable: "Neighborhood",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Business",
                table: "FederativeUnit",
                columns: new[] { "Id", "Abbreviation" },
                values: new object[,]
                {
                    { 1, "AC" },
                    { 25, "SP" },
                    { 24, "SC" },
                    { 23, "RR" },
                    { 22, "RO" },
                    { 21, "RS" },
                    { 20, "RN" },
                    { 19, "RJ" },
                    { 18, "PI" },
                    { 17, "PE" },
                    { 16, "PR" },
                    { 15, "PB" },
                    { 26, "SE" },
                    { 14, "PA" },
                    { 12, "MS" },
                    { 11, "MT" },
                    { 10, "MA" },
                    { 9, "GO" },
                    { 8, "ES" },
                    { 7, "DF" },
                    { 6, "CE" },
                    { 5, "BA" },
                    { 4, "AM" },
                    { 3, "AP" },
                    { 2, "AL" },
                    { 13, "MG" },
                    { 27, "TO" }
                });

            migrationBuilder.CreateIndex(
                name: "idx_address_neighborhoodid",
                schema: "Business",
                table: "Address",
                column: "NeighborhoodId");

            migrationBuilder.CreateIndex(
                name: "idx_address_postalcode",
                schema: "Business",
                table: "Address",
                column: "PostalCode");

            migrationBuilder.CreateIndex(
                name: "idx_city_federativeunitid",
                schema: "Business",
                table: "City",
                column: "FederativeUnitId");

            migrationBuilder.CreateIndex(
                name: "idx_neighborhood_cityid",
                schema: "Business",
                table: "Neighborhood",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Address",
                schema: "Business");

            migrationBuilder.DropTable(
                name: "Neighborhood",
                schema: "Business");

            migrationBuilder.DropTable(
                name: "City",
                schema: "Business");

            migrationBuilder.DropTable(
                name: "FederativeUnit",
                schema: "Business");
        }
    }
}
