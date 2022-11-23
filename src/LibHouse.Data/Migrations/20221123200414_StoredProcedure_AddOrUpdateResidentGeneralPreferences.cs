using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Text;

namespace LibHouse.Data.Migrations
{
    public partial class StoredProcedure_AddOrUpdateResidentGeneralPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            StringBuilder storedProcedureCode = new();
            storedProcedureCode.Append("CREATE PROCEDURE [Business].[sp_residentpreferences_addOrUpdateResidentGeneralPreferences]" + Environment.NewLine);
            storedProcedureCode.Append("@ResidentId uniqueidentifier," + Environment.NewLine);
            storedProcedureCode.Append("@WantSpaceForAnimals bit," + Environment.NewLine);
            storedProcedureCode.Append("@AcceptChildren bit," + Environment.NewLine);
            storedProcedureCode.Append("@WantsToParty bit," + Environment.NewLine);
            storedProcedureCode.Append("@AcceptsOnlyFemaleRoommates bit," + Environment.NewLine);
            storedProcedureCode.Append("@AcceptsOnlyMaleRoommates bit," + Environment.NewLine);
            storedProcedureCode.Append("@MaximumNumberOfRoommatesDesired tinyint," + Environment.NewLine);
            storedProcedureCode.Append("@MinimumNumberOfRoommatesDesired tinyint," + Environment.NewLine);
            storedProcedureCode.Append("@AcceptSmokers bit" + Environment.NewLine);
            storedProcedureCode.Append("AS" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append(@"IF NOT EXISTS(SELECT TOP 1 ResidentId FROM [Business].[ResidentPreferences] WHERE ResidentId = @ResidentId)" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append(@"INSERT INTO [Business].[ResidentPreferences] ([ResidentId],[GeneralPreferences_Animal_WantSpaceForAnimals],[GeneralPreferences_Children_AcceptChildren],[GeneralPreferences_Party_WantsToParty],[GeneralPreferences_Roommate_AcceptsOnlyFemaleRoommates],[GeneralPreferences_Roommate_AcceptsOnlyMaleRoommates],[GeneralPreferences_Roommate_MaximumNumberOfRoommatesDesired],[GeneralPreferences_Roommate_MinimumNumberOfRoommatesDesired],[GeneralPreferences_Smokers_AcceptSmokers])" + Environment.NewLine);
            storedProcedureCode.Append("VALUES(@ResidentId,@WantSpaceForAnimals,@AcceptChildren,@WantsToParty,@AcceptsOnlyFemaleRoommates,@AcceptsOnlyMaleRoommates,@MaximumNumberOfRoommatesDesired,@MinimumNumberOfRoommatesDesired,@AcceptSmokers)" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            storedProcedureCode.Append("ELSE" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append("UPDATE [Business].[ResidentPreferences]" + Environment.NewLine);
            storedProcedureCode.Append("SET [GeneralPreferences_Animal_WantSpaceForAnimals] = @WantSpaceForAnimals," + Environment.NewLine);
            storedProcedureCode.Append("[GeneralPreferences_Children_AcceptChildren] = @AcceptChildren," + Environment.NewLine);
            storedProcedureCode.Append("[GeneralPreferences_Party_WantsToParty] = @WantsToParty," + Environment.NewLine);
            storedProcedureCode.Append("[GeneralPreferences_Roommate_AcceptsOnlyFemaleRoommates] = @AcceptsOnlyFemaleRoommates," + Environment.NewLine);
            storedProcedureCode.Append("[GeneralPreferences_Roommate_AcceptsOnlyMaleRoommates] = @AcceptsOnlyMaleRoommates," + Environment.NewLine);
            storedProcedureCode.Append("[GeneralPreferences_Roommate_MaximumNumberOfRoommatesDesired] = @MaximumNumberOfRoommatesDesired," + Environment.NewLine);
            storedProcedureCode.Append("[GeneralPreferences_Roommate_MinimumNumberOfRoommatesDesired] = @MinimumNumberOfRoommatesDesired," + Environment.NewLine);
            storedProcedureCode.Append("[GeneralPreferences_Smokers_AcceptSmokers] = @AcceptSmokers" + Environment.NewLine);
            storedProcedureCode.Append("WHERE [ResidentId] = @ResidentId" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            migrationBuilder.Sql(storedProcedureCode.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [Business].[sp_residentpreferences_addOrUpdateResidentGeneralPreferences]");
        }
    }
}