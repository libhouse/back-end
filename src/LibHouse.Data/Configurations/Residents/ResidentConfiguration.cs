using LibHouse.Business.Entities.Residents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibHouse.Data.Configurations.Residents
{
    internal class ResidentConfiguration : IEntityTypeConfiguration<Resident>
    {
        public void Configure(EntityTypeBuilder<Resident> builder)
        {
            builder.ToTable("Residents", "Business");
        }
    }
}