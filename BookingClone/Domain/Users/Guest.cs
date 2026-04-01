using BookingClone.Domain.Bookings;

namespace BookingClone.Domain.Users;

public class Guest
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
