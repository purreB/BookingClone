using BookingClone.Domain.Hotels;

namespace BookingClone.Domain.Users;

public class StaffUser
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsOwner { get; set; }
    public ICollection<Hotel> OwnedHotels { get; set; } = new List<Hotel>();
}
