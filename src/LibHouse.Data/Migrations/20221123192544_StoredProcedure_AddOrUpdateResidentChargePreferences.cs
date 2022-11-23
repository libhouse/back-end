using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Text;

namespace LibHouse.Data.Migrations
{
    public partial class StoredProcedure_AddOrUpdateResidentChargePreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            StringBuilder storedProcedureCode = new();
            storedProcedureCode.Append("CREATE PROCEDURE [Business].[sp_residentpreferences_addOrUpdateResidentChargePreferences]" + Environment.NewLine);
            storedProcedureCode.Append("@ResidentId uniqueidentifier," + Environment.NewLine);
            storedProcedureCode.Append("@MinimumExpenseAmountDesired decimal(6,2)," + Environment.NewLine);
            storedProcedureCode.Append("@MaximumExpenseAmountDesired decimal(6,2)," + Environment.NewLine);
            storedProcedureCode.Append("@MinimumRentAmountDesired decimal(6,2)," + Environment.NewLine);
            storedProcedureCode.Append("@MaximumRentAmountDesired decimal(6,2)" + Environment.NewLine);
            storedProcedureCode.Append("AS" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append(@"IF NOT EXISTS(SELECT TOP 1 ResidentId FROM [Business].[ResidentPreferences] WHERE ResidentId = @ResidentId)" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append(@"INSERT INTO [Business].[ResidentPreferences] ([ResidentId],[ChargePreferences_Expense_MinimumExpenseAmountDesired],[ChargePreferences_Expense_MaximumExpenseAmountDesired],[ChargePreferences_Rent_MinimumRentAmountDesired],[ChargePreferences_Rent_MaximumRentAmountDesired])" + Environment.NewLine);
            storedProcedureCode.Append("VALUES(@ResidentId,@MinimumExpenseAmountDesired,@MaximumExpenseAmountDesired,@MinimumRentAmountDesired,@MaximumRentAmountDesired)" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            storedProcedureCode.Append("ELSE" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append("UPDATE [Business].[ResidentPreferences]" + Environment.NewLine);
            storedProcedureCode.Append("SET [ChargePreferences_Expense_MinimumExpenseAmountDesired] = @MinimumExpenseAmountDesired," + Environment.NewLine);
            storedProcedureCode.Append("[ChargePreferences_Expense_MaximumExpenseAmountDesired] = @MaximumExpenseAmountDesired," + Environment.NewLine);
            storedProcedureCode.Append("[ChargePreferences_Rent_MinimumRentAmountDesired] = @MinimumRentAmountDesired," + Environment.NewLine);
            storedProcedureCode.Append("[ChargePreferences_Rent_MaximumRentAmountDesired]  = @MaximumRentAmountDesired" + Environment.NewLine);
            storedProcedureCode.Append("WHERE [ResidentId] = @ResidentId" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            migrationBuilder.Sql(storedProcedureCode.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [Business].[sp_residentpreferences_addOrUpdateResidentChargePreferences]");
        }
    }
}