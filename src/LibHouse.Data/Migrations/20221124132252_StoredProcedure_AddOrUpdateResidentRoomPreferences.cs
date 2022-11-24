using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Text;

namespace LibHouse.Data.Migrations
{
    public partial class StoredProcedure_AddOrUpdateResidentRoomPreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            StringBuilder storedProcedureCode = new();
            storedProcedureCode.Append("CREATE PROCEDURE [Business].[sp_residentpreferences_addOrUpdateResidentRoomPreferences]" + Environment.NewLine);
            storedProcedureCode.Append("@ResidentId uniqueidentifier," + Environment.NewLine);
            storedProcedureCode.Append("@DormitoryType varchar(6)," + Environment.NewLine);
            storedProcedureCode.Append("@RequireFurnishedDormitory bit," + Environment.NewLine);
            storedProcedureCode.Append("@BathroomType varchar(6)," + Environment.NewLine);
            storedProcedureCode.Append("@GarageIsRequired bit," + Environment.NewLine);
            storedProcedureCode.Append("@StoveIsRequired bit," + Environment.NewLine);
            storedProcedureCode.Append("@MicrowaveIsRequired bit," + Environment.NewLine);
            storedProcedureCode.Append("@RefrigeratorIsRequired bit," + Environment.NewLine);
            storedProcedureCode.Append("@ServiceAreaIsRequired bit," + Environment.NewLine);
            storedProcedureCode.Append("@RecreationAreaIsRequired bit" + Environment.NewLine);
            storedProcedureCode.Append("AS" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append(@"IF NOT EXISTS(SELECT TOP 1 ResidentId FROM [Business].[ResidentPreferences] WHERE ResidentId = @ResidentId)" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append(@"INSERT INTO [Business].[ResidentPreferences] ([ResidentId],[RoomPreferences_Dormitory_DormitoryType],[RoomPreferences_Dormitory_RequireFurnishedDormitory],[RoomPreferences_Bathroom_BathroomType],[RoomPreferences_Garage_GarageIsRequired],[RoomPreferences_Kitchen_StoveIsRequired],[RoomPreferences_Kitchen_MicrowaveIsRequired],[RoomPreferences_Kitchen_RefrigeratorIsRequired],[RoomPreferences_Other_ServiceAreaIsRequired],[RoomPreferences_Other_RecreationAreaIsRequired])" + Environment.NewLine);
            storedProcedureCode.Append("VALUES(@ResidentId,@DormitoryType,@RequireFurnishedDormitory,@BathroomType,@GarageIsRequired,@StoveIsRequired,@MicrowaveIsRequired,@RefrigeratorIsRequired,@ServiceAreaIsRequired,@RecreationAreaIsRequired)" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            storedProcedureCode.Append("ELSE" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append("UPDATE [Business].[ResidentPreferences]" + Environment.NewLine);
            storedProcedureCode.Append("SET [RoomPreferences_Dormitory_DormitoryType] = @DormitoryType," + Environment.NewLine);
            storedProcedureCode.Append("[RoomPreferences_Dormitory_RequireFurnishedDormitory] = @RequireFurnishedDormitory," + Environment.NewLine);
            storedProcedureCode.Append("[RoomPreferences_Bathroom_BathroomType] = @BathroomType," + Environment.NewLine);
            storedProcedureCode.Append("[RoomPreferences_Garage_GarageIsRequired] = @GarageIsRequired," + Environment.NewLine);
            storedProcedureCode.Append("[RoomPreferences_Kitchen_StoveIsRequired] = @StoveIsRequired," + Environment.NewLine);
            storedProcedureCode.Append("[RoomPreferences_Kitchen_MicrowaveIsRequired] = @MicrowaveIsRequired," + Environment.NewLine);
            storedProcedureCode.Append("[RoomPreferences_Kitchen_RefrigeratorIsRequired] = @RefrigeratorIsRequired," + Environment.NewLine);
            storedProcedureCode.Append("[RoomPreferences_Other_ServiceAreaIsRequired] = @ServiceAreaIsRequired," + Environment.NewLine);
            storedProcedureCode.Append("[RoomPreferences_Other_RecreationAreaIsRequired] = @RecreationAreaIsRequired" + Environment.NewLine);
            storedProcedureCode.Append("WHERE [ResidentId] = @ResidentId" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            migrationBuilder.Sql(storedProcedureCode.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [Business].[sp_residentpreferences_addOrUpdateResidentRoomPreferences]");
        }
    }
}