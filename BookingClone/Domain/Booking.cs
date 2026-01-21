using System;

namespace BookingClone.Domain
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid GuestId { get; set; }
        public Guest Guest { get; set; }
        public Guid HotelRoomId { get; set; }
        public HotelRoom HotelRoom { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        // Additional properties can be added as needed
    }
}