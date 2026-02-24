namespace BookingClone.Domain;

public class Hotel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public Guid OwnerId { get; set; }
    public StaffUser Owner { get; set; } = null!;
    public List<HotelRoom> Rooms { get; set; } = [];
}
