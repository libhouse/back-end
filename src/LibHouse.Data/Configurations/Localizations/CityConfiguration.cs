using LibHouse.Business.Entities.Localizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibHouse.Data.Configurations.Localizations
{
    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("City", "Business");
            builder.Property<int>("Id").HasColumnType("int").ValueGeneratedOnAdd();
            builder.HasKey("Id");
            builder.Property("Name").HasColumnType("varchar").HasMaxLength(30).HasColumnName("Description").IsRequired();
            builder.HasOne("FederativeUnit").WithMany().HasForeignKey("FederativeUnitId").IsRequired();
            builder.HasIndex("FederativeUnitId").HasDatabaseName("idx_city_federativeunitid");
            builder.HasIndex("FederativeUnitId", "Name").IsUnique().HasDatabaseName("idx_city_federativeunitid_description");
        }
    }
}