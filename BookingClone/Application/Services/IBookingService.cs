using System.Collections.Generic;
using BookingClone.Application.DTOs;

namespace BookingClone.Application.Services
{
    public interface IBookingService
    {
        IEnumerable<BookingDto> GetAllBookings();
        BookingDto GetBookingById(Guid id);
        void CreateBooking(BookingDto bookingDto);
        void UpdateBooking(BookingDto bookingDto);
        void DeleteBooking(Guid id);
    }
}