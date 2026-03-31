using TypeGen.Core.TypeAnnotations;
namespace BookingClone.Application.DTOs;

using System;

[ExportTsClass]
public class BookingDto
{
    public Guid Id { get; set; }
    public Guid GuestId { get; set; }
    public Guid HotelRoomId { get; set; }
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
}