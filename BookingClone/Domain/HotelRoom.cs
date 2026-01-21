namespace BookingClone.Domain;

public class HotelRoom
{
    public Guid Id { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public bool IsAvailable { get; set; }
    public Guid HotelId { get; set; }
    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
