using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibHouse.Infrastructure.Authentication.Context.Configurations
{
    public class IdentityUserTokenConfiguration : IEntityTypeConfiguration<IdentityUserToken<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserToken<string>> builder)
        {
            builder.ToTable("AspNetUserTokens", "Authentication");
        }
    }
}