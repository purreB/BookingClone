namespace BookingClone.Application.DTOs;

using System;

public class BookingDto
{
    public Guid Id { get; set; }
    public Guid GuestId { get; set; }
    public Guid HotelRoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    // Additional properties can be added as needed
}