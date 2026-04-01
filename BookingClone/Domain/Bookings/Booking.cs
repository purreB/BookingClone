using BookingClone.Domain.Hotels;
using BookingClone.Domain.Users;

namespace BookingClone.Domain.Bookings;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid GuestId { get; set; }
    public Guest Guest { get; set; } = null!;
    public Guid HotelRoomId { get; set; }
    public HotelRoom HotelRoom { get; set; } = null!;
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public decimal TotalPrice { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
}

public enum BookingStatus
{
    Pending,
    Confirmed,
    CheckedIn,
    CheckedOut,
    Cancelled
}
