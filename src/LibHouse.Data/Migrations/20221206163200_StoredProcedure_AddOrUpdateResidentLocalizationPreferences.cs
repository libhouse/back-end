using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Text;

namespace LibHouse.Data.Migrations
{
    public partial class StoredProcedure_AddOrUpdateResidentLocalizationPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            StringBuilder storedProcedureCode = new();
            storedProcedureCode.Append("CREATE PROCEDURE [Business].[sp_residentpreferences_addOrUpdateResidentLocalizationPreferences]" + Environment.NewLine);
            storedProcedureCode.Append("@ResidentId uniqueidentifier," + Environment.NewLine);
            storedProcedureCode.Append("@LandmarkAddressId uniqueidentifier" + Environment.NewLine);
            storedProcedureCode.Append("AS" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append(@"IF NOT EXISTS(SELECT TOP 1 ResidentId FROM [Business].[ResidentPreferences] WHERE ResidentId = @ResidentId)" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append(@"INSERT INTO [Business].[ResidentPreferences] ([ResidentId],[LocalizationPreferences_Landmark_LandmarkAddressId])" + Environment.NewLine);
            storedProcedureCode.Append("VALUES(@ResidentId,@LandmarkAddressId)" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            storedProcedureCode.Append("ELSE" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append("UPDATE [Business].[ResidentPreferences]" + Environment.NewLine);
            storedProcedureCode.Append("SET [LocalizationPreferences_Landmark_LandmarkAddressId] = @LandmarkAddressId" + Environment.NewLine);
            storedProcedureCode.Append("WHERE [ResidentId] = @ResidentId" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            migrationBuilder.Sql(storedProcedureCode.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [Business].[sp_residentpreferences_addOrUpdateResidentLocalizationPreferences]");
        }
    }
}