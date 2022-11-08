using LibHouse.Infrastructure.Authentication.Token.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibHouse.Infrastructure.Authentication.Context.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("AspNetRefreshTokens", "Authentication");
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Token).HasColumnType("char(71)").IsRequired();
            builder.Property(r => r.UserId).HasColumnType("nvarchar(450)").IsRequired();
            builder.Property(r => r.JwtId).HasColumnType("varchar(100)").IsRequired();
            builder.Property(r => r.IsUsed).HasColumnType("bit").HasDefaultValueSql("0").IsRequired();
            builder.Property(r => r.IsRevoked).HasColumnType("bit").HasDefaultValueSql("0").IsRequired();
            builder.Property(r => r.CreatedAt).HasColumnType("datetime").HasDefaultValueSql("GETDATE()").IsRequired();
            builder.Property(r => r.ExpiresIn).HasColumnType("datetime").IsRequired();
            builder.Property(r => r.RevokedAt).HasColumnType("datetime");
            builder.HasOne<IdentityUser>().WithMany().HasForeignKey(r => r.UserId);
            builder.HasIndex(r => r.Token).HasDatabaseName("idx_refresh_token").IsUnique();
            builder.HasIndex(r => r.JwtId).HasDatabaseName("idx_jwt_id").IsUnique();
        }
    }
}