using LibHouse.Business.Entities.Localizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibHouse.Data.Configurations.Localizations
{
    internal class FederativeUnitConfiguration : IEntityTypeConfiguration<FederativeUnit>
    {
        public void Configure(EntityTypeBuilder<FederativeUnit> builder)
        {
            builder.ToTable("FederativeUnit", "Business");
            builder.Property<int>("Id").HasColumnType("int").ValueGeneratedOnAdd();
            builder.HasKey("Id");
            builder.Property("AbbreviationOfTheFederativeUnit").HasColumnType("char").HasMaxLength(2).HasColumnName("Abbreviation").IsRequired();
            builder.HasData(new[]
            {
                new { Id = 1, AbbreviationOfTheFederativeUnit = "AC" },
                new { Id = 2, AbbreviationOfTheFederativeUnit = "AL" },
                new { Id = 3, AbbreviationOfTheFederativeUnit = "AP" },
                new { Id = 4, AbbreviationOfTheFederativeUnit = "AM" },
                new { Id = 5, AbbreviationOfTheFederativeUnit = "BA" },
                new { Id = 6, AbbreviationOfTheFederativeUnit = "CE" },
                new { Id = 7, AbbreviationOfTheFederativeUnit = "DF" },
                new { Id = 8, AbbreviationOfTheFederativeUnit = "ES" },
                new { Id = 9, AbbreviationOfTheFederativeUnit = "GO" },
                new { Id = 10, AbbreviationOfTheFederativeUnit = "MA" },
                new { Id = 11, AbbreviationOfTheFederativeUnit = "MT" },
                new { Id = 12, AbbreviationOfTheFederativeUnit = "MS" },
                new { Id = 13, AbbreviationOfTheFederativeUnit = "MG" },
                new { Id = 14, AbbreviationOfTheFederativeUnit = "PA" },
                new { Id = 15, AbbreviationOfTheFederativeUnit = "PB" },
                new { Id = 16, AbbreviationOfTheFederativeUnit = "PR" },
                new { Id = 17, AbbreviationOfTheFederativeUnit = "PE" },
                new { Id = 18, AbbreviationOfTheFederativeUnit = "PI" },
                new { Id = 19, AbbreviationOfTheFederativeUnit = "RJ" },
                new { Id = 20, AbbreviationOfTheFederativeUnit = "RN" },
                new { Id = 21, AbbreviationOfTheFederativeUnit = "RS" },
                new { Id = 22, AbbreviationOfTheFederativeUnit = "RO" },
                new { Id = 23, AbbreviationOfTheFederativeUnit = "RR" },
                new { Id = 24, AbbreviationOfTheFederativeUnit = "SC" },
                new { Id = 25, AbbreviationOfTheFederativeUnit = "SP" },
                new { Id = 26, AbbreviationOfTheFederativeUnit = "SE" },
                new { Id = 27, AbbreviationOfTheFederativeUnit = "TO" },
            });
        }
    }
}