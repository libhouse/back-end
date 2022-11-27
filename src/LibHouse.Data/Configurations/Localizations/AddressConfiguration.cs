using LibHouse.Business.Entities.Localizations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibHouse.Data.Configurations.Localizations
{
    internal class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address", "Business");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(a => a.UpdatedAt).HasColumnType("datetime").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate().IsRequired();
            builder.Property("Name").HasColumnType("varchar").HasMaxLength(60).HasColumnName("Description").IsRequired();
            builder.OwnsOne<AddressNumber>("AddressNumber", addressNumber =>
            {
                addressNumber.Property("Number").HasColumnType("smallint").HasColumnName("Number").IsRequired();
            });
            builder.OwnsOne<AddressComplement>("AddressComplement", addressComplement =>
            {
                addressComplement.Property("Description").HasColumnType("varchar").HasMaxLength(255).HasColumnName("Complement");
            });
            builder.OwnsOne<PostalCode>("PostalCode", postalCode =>
            {
                postalCode.Property("PostalCodeNumber").HasColumnType("char").HasMaxLength(8).HasColumnName("PostalCode").IsRequired();
                postalCode.HasIndex("PostalCodeNumber").HasDatabaseName("idx_address_postalcode");
            });
            builder.HasOne("Neighborhood").WithMany().HasForeignKey("NeighborhoodId").IsRequired();
            builder.HasIndex("NeighborhoodId").HasDatabaseName("idx_address_neighborhoodid");
        }
    }
}