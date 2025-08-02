using FarmEase.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FarmEase.Infrastructure.Data
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
        {
        }

        public DbSet<Farm> Farms { get; set; }
        public DbSet<FarmRoom> FarmRooms { get; set; }
        public DbSet<Amenity> Amenities { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
        }
    }
}
