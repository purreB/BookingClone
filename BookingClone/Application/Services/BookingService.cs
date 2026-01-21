using System.Collections.Generic;
using BookingClone.Application.DTOs;
using BookingClone.Domain;
using BookingClone.Infrastructure.Repositories;

namespace BookingClone.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingService(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

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
            _bookingRepository.Delete(id);
        }
    }
}