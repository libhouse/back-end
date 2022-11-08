using LibHouse.Business.Entities.Owners;
using LibHouse.Business.Entities.Residents;
using LibHouse.Business.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace LibHouse.Data.Context
{
    public class LibHouseContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Resident> Residents { get; set; }
        public DbSet<Owner> Owners { get; set; }

        public LibHouseContext(DbContextOptions<LibHouseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibHouseContext).Assembly);
        }
    }
}