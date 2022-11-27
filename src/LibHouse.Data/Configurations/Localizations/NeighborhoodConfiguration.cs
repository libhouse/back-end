using LibHouse.Business.Entities.Localizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibHouse.Data.Configurations.Localizations
{
    internal class NeighborhoodConfiguration : IEntityTypeConfiguration<Neighborhood>
    {
        public void Configure(EntityTypeBuilder<Neighborhood> builder)
        {
            builder.ToTable("Neighborhood", "Business");
            builder.Property<int>("Id").HasColumnType("int").ValueGeneratedOnAdd();
            builder.HasKey("Id");
            builder.Property("Name").HasColumnType("varchar").HasMaxLength(60).HasColumnName("Description").IsRequired();
            builder.HasOne("City").WithMany().HasForeignKey("CityId").IsRequired();
            builder.HasIndex("CityId").HasDatabaseName("idx_neighborhood_cityid");
        }
    }
}