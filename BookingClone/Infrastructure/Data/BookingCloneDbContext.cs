using Microsoft.EntityFrameworkCore;
using BookingClone.Domain;

namespace BookingClone.Infrastructure.Data;

public class BookingCloneDbContext(DbContextOptions<BookingCloneDbContext> options) : DbContext(options)
{
    public DbSet<Hotel> Hotels { get; set; } = null!;
    public DbSet<HotelRoom> HotelRooms { get; set; } = null!;
    public DbSet<Booking> Bookings { get; set; } = null!;
    public DbSet<Guest> Guests { get; set; } = null!;
    public DbSet<StaffUser> StaffUsers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Hotel aggregate root
        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.HasKey(h => h.Id);
            entity.Property(h => h.Name).IsRequired().HasMaxLength(255);
            entity.Property(h => h.Address).IsRequired().HasMaxLength(500);
            entity.HasOne(h => h.Owner)
                .WithMany(s => s.OwnedHotels)
                .HasForeignKey(h => h.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasMany(h => h.Rooms)
                .WithOne(r => r.Hotel)
                .HasForeignKey(r => r.HotelId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure HotelRoom
        modelBuilder.Entity<HotelRoom>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.RoomNumber).IsRequired().HasMaxLength(50);
            entity.Property(r => r.Type).IsRequired().HasMaxLength(50);
        });

        // Configure Booking aggregate root
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(b => b.Id);
            entity.Property(b => b.Status).IsRequired();
            entity.Property(b => b.TotalPrice).HasPrecision(18, 2);
            entity.HasOne(b => b.Guest)
                .WithMany(g => g.Bookings)
                .HasForeignKey(b => b.GuestId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(b => b.HotelRoom)
                .WithMany(r => r.Bookings)
                .HasForeignKey(b => b.HotelRoomId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Guest
        modelBuilder.Entity<Guest>(entity =>
        {
            entity.HasKey(g => g.Id);
            entity.Property(g => g.Name).IsRequired().HasMaxLength(255);
            entity.Property(g => g.Email).IsRequired().HasMaxLength(255);
        });

        // Configure StaffUser
        modelBuilder.Entity<StaffUser>(entity =>
        {
            entity.HasKey(s => s.Id);
            entity.Property(s => s.Name).IsRequired().HasMaxLength(255);
            entity.Property(s => s.Email).IsRequired().HasMaxLength(255);
        });
    }
}
