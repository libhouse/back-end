using LibHouse.Infrastructure.Authentication.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibHouse.Infrastructure.Authentication.Context.Configurations
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.ToTable("AspNetRoles", "Authentication");

            builder.HasData(new IdentityRole[]
            {
                new IdentityRole(LibHouseUserRole.User)
                {
                    NormalizedName = LibHouseUserRole.User.ToUpper()
                },
                new IdentityRole(LibHouseUserRole.Resident)
                {
                    NormalizedName = LibHouseUserRole.Resident.ToUpper()
                },
                new IdentityRole(LibHouseUserRole.Owner)
                {
                    NormalizedName = LibHouseUserRole.Owner.ToUpper()
                },
            });
        }
    }
}