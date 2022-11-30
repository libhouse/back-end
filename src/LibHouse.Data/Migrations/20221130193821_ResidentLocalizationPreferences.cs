using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LibHouse.Data.Migrations
{
    public partial class ResidentLocalizationPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LocalizationPreferences_Landmark_LandmarkAddressId",
                schema: "Business",
                table: "ResidentPreferences",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "idx_residentpreferences_landmarkaddressid",
                schema: "Business",
                table: "ResidentPreferences",
                column: "LocalizationPreferences_Landmark_LandmarkAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_ResidentPreferences_Address_LocalizationPreferences_Landmark_LandmarkAddressId",
                schema: "Business",
                table: "ResidentPreferences",
                column: "LocalizationPreferences_Landmark_LandmarkAddressId",
                principalSchema: "Business",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ResidentPreferences_Address_LocalizationPreferences_Landmark_LandmarkAddressId",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropIndex(
                name: "idx_residentpreferences_landmarkaddressid",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropColumn(
                name: "LocalizationPreferences_Landmark_LandmarkAddressId",
                schema: "Business",
                table: "ResidentPreferences");
        }
    }
}