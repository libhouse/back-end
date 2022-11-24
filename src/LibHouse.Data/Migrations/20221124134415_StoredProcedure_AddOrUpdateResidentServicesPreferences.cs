using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Text;

namespace LibHouse.Data.Migrations
{
    public partial class StoredProcedure_AddOrUpdateResidentServicesPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            StringBuilder storedProcedureCode = new();
            storedProcedureCode.Append("CREATE PROCEDURE [Business].[sp_residentpreferences_addOrUpdateResidentServicesPreferences]" + Environment.NewLine);
            storedProcedureCode.Append("@ResidentId uniqueidentifier," + Environment.NewLine);
            storedProcedureCode.Append("@HouseCleaningIsRequired bit," + Environment.NewLine);
            storedProcedureCode.Append("@InternetServiceIsRequired bit," + Environment.NewLine);
            storedProcedureCode.Append("@CableTelevisionIsRequired bit" + Environment.NewLine);
            storedProcedureCode.Append("AS" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append(@"IF NOT EXISTS(SELECT TOP 1 ResidentId FROM [Business].[ResidentPreferences] WHERE ResidentId = @ResidentId)" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append(@"INSERT INTO [Business].[ResidentPreferences] ([ResidentId],[ServicesPreferences_Cleaning_HouseCleaningIsRequired],[ServicesPreferences_Internet_InternetServiceIsRequired],[ServicesPreferences_Television_CableTelevisionIsRequired])" + Environment.NewLine);
            storedProcedureCode.Append("VALUES(@ResidentId,@HouseCleaningIsRequired,@InternetServiceIsRequired,@CableTelevisionIsRequired)" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            storedProcedureCode.Append("ELSE" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append("UPDATE [Business].[ResidentPreferences]" + Environment.NewLine);
            storedProcedureCode.Append("SET [ServicesPreferences_Cleaning_HouseCleaningIsRequired] = @HouseCleaningIsRequired," + Environment.NewLine);
            storedProcedureCode.Append("[ServicesPreferences_Internet_InternetServiceIsRequired] = @InternetServiceIsRequired," + Environment.NewLine);
            storedProcedureCode.Append("[ServicesPreferences_Television_CableTelevisionIsRequired] = @CableTelevisionIsRequired" + Environment.NewLine);
            storedProcedureCode.Append("WHERE [ResidentId] = @ResidentId" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            migrationBuilder.Sql(storedProcedureCode.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [Business].[sp_residentpreferences_addOrUpdateResidentServicesPreferences]");
        }
    }
}