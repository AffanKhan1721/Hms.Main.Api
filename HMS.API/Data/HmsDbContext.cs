using HMS.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HMS.API.Data;

public class HmsDbContext : DbContext
{
    public HmsDbContext(DbContextOptions<HmsDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Room> Rooms { get; set; } = null!;
    public DbSet<Reservation> Reservations { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Customer)
            .WithMany()
            .HasForeignKey(r => r.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Reservation>()
            .HasOne(r => r.Room)
            .WithMany()
            .HasForeignKey(r => r.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<User>().HasData(
            new User
            {
                UserId = 1,
                FullName = "Admin",
                Email = "admin@hotel.com",
                PasswordHash = "$2a$10$eQB9OoO4ZKwlQg/K3heNa.aRREgJKqAob.GdeLk/z6mcbzAOEQlny",
                PhoneNumber = "123-456-7890",
                Role = "Admin"
            }
        );
    }
}
