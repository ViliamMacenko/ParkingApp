using Microsoft.EntityFrameworkCore;
using ParkingApp.Models;

namespace ParkingApp.Context
{
    public class ApplicationContext : DbContext
    {
        public virtual DbSet<Reservation> Reservations { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Reservations)
                .WithOne(b => b.User)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
