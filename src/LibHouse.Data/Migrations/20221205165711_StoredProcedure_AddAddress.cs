using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Text;

namespace LibHouse.Data.Migrations
{
    public partial class StoredProcedure_AddAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            StringBuilder storedProcedureCode = new();
            storedProcedureCode.Append("CREATE PROCEDURE [Business].[sp_address_addAddress]" + Environment.NewLine);
            storedProcedureCode.Append("@AddressId uniqueidentifier," + Environment.NewLine);
            storedProcedureCode.Append("@Description varchar(60)," + Environment.NewLine);
            storedProcedureCode.Append("@Number smallint," + Environment.NewLine);
            storedProcedureCode.Append("@Complement varchar(255) = null," + Environment.NewLine);
            storedProcedureCode.Append("@Neighborhood varchar(60)," + Environment.NewLine);
            storedProcedureCode.Append("@PostalCode char(8)," + Environment.NewLine);
            storedProcedureCode.Append("@City varchar(30)," + Environment.NewLine);
            storedProcedureCode.Append("@FederativeUnit char(2)" + Environment.NewLine);
            storedProcedureCode.Append("AS" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append("DECLARE @FederativeUnitId int = 0" + Environment.NewLine);
            storedProcedureCode.Append("DECLARE @CityId int = 0" + Environment.NewLine);
            storedProcedureCode.Append("DECLARE @NeighborhoodId int = 0" + Environment.NewLine);
            storedProcedureCode.Append("SELECT @FederativeUnitId = Id FROM [Business].[FederativeUnit] WHERE Abbreviation = @FederativeUnit" + Environment.NewLine);
            storedProcedureCode.Append("SELECT @CityId = Id FROM [Business].[City] WHERE FederativeUnitId = @FederativeUnitId And Description = @City" + Environment.NewLine);
            storedProcedureCode.Append("IF (@CityId = 0)" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append("INSERT INTO [Business].[City](Description, FederativeUnitId) VALUES(@City, @FederativeUnitId)" + Environment.NewLine);
            storedProcedureCode.Append("SELECT @CityId = SCOPE_IDENTITY()" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            storedProcedureCode.Append("SELECT @NeighborhoodId = Id FROM [Business].[Neighborhood] WHERE CityId = @CityId And Description = @Neighborhood" + Environment.NewLine);
            storedProcedureCode.Append("IF (@NeighborhoodId = 0)" + Environment.NewLine);
            storedProcedureCode.Append("BEGIN" + Environment.NewLine);
            storedProcedureCode.Append("INSERT INTO [Business].[Neighborhood](Description, CityId) VALUES(@Neighborhood, @CityId)" + Environment.NewLine);
            storedProcedureCode.Append("SELECT @NeighborhoodId = SCOPE_IDENTITY()" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            storedProcedureCode.Append("INSERT INTO [Business].[Address] (Id, Description, Number, Complement, NeighborhoodId, PostalCode, Active)" + Environment.NewLine);
            storedProcedureCode.Append("VALUES(@AddressId, @Description, @Number, @Complement, @NeighborhoodId, @PostalCode, 1)" + Environment.NewLine);
            storedProcedureCode.Append("END" + Environment.NewLine);
            migrationBuilder.Sql(storedProcedureCode.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE [Business].[sp_address_addAddress]");
        }
    }
}