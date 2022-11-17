using Microsoft.EntityFrameworkCore.Migrations;

namespace LibHouse.Data.Migrations
{
    public partial class ResidentChargePreferences : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ChargePreferences_Expense_MaximumExpenseAmountDesired",
                schema: "Business",
                table: "ResidentPreferences",
                type: "decimal(6,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ChargePreferences_Expense_MinimumExpenseAmountDesired",
                schema: "Business",
                table: "ResidentPreferences",
                type: "decimal(6,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ChargePreferences_Rent_MaximumRentAmountDesired",
                schema: "Business",
                table: "ResidentPreferences",
                type: "decimal(6,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "ChargePreferences_Rent_MinimumRentAmountDesired",
                schema: "Business",
                table: "ResidentPreferences",
                type: "decimal(6,2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargePreferences_Expense_MaximumExpenseAmountDesired",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropColumn(
                name: "ChargePreferences_Expense_MinimumExpenseAmountDesired",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropColumn(
                name: "ChargePreferences_Rent_MaximumRentAmountDesired",
                schema: "Business",
                table: "ResidentPreferences");

            migrationBuilder.DropColumn(
                name: "ChargePreferences_Rent_MinimumRentAmountDesired",
                schema: "Business",
                table: "ResidentPreferences");
        }
    }
}