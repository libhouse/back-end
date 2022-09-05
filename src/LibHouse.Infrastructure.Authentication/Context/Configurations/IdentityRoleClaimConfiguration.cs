using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibHouse.Infrastructure.Authentication.Context.Configurations
{
    public class IdentityRoleClaimConfiguration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
        {
            builder.ToTable("AspNetRoleClaims", "Authentication");
        }
    }
}