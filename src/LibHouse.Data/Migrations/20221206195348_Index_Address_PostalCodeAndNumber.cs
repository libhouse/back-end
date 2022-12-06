using Microsoft.EntityFrameworkCore.Migrations;

namespace LibHouse.Data.Migrations
{
    public partial class Index_Address_PostalCodeAndNumber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE NONCLUSTERED INDEX [idx_address_postalcode_number] ON [Business].[Address]([PostalCode],[Number] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX [idx_address_postalcode_number] ON [Business].[Address]");
        }
    }
}