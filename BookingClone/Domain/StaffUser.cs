namespace BookingClone.Domain;

public class StaffUser
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PasswordHash { get; set; }
    public bool IsOwner { get; set; }
    public ICollection<Hotel> OwnedHotels { get; set; } = new List<Hotel>();
}
