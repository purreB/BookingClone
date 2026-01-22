using System.Collections.Generic;
using BookingClone.Application.DTOs;
using BookingClone.Domain;
using BookingClone.Infrastructure.Repositories;

namespace BookingClone.Application.Services
{
    public class BookingService(IBookingRepository bookingRepository) : IBookingService
    {

        public IEnumerable<BookingDto> GetAllBookings()
        {
            // Map Booking to BookingDto
            return new List<BookingDto>();
        }

        public BookingDto GetBookingById(Guid id)
        {
            // Map Booking to BookingDto
            return new BookingDto();
        }

        public void CreateBooking(BookingDto bookingDto)
        {
            // Map BookingDto to Booking and save
        }

        public void UpdateBooking(BookingDto bookingDto)
        {
            // Map BookingDto to Booking and update
        }

        public void DeleteBooking(Guid id)
        {
            bookingRepository.Delete(id);
        }
    }
}