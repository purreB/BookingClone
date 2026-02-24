using System.Collections.Generic;
using BookingClone.Application.DTOs;

namespace BookingClone.Application.Services;

public interface IBookingService
{
    Task<IEnumerable<BookingDto>> GetAllBookingsAsync();
    Task<BookingDto?> GetBookingByIdAsync(Guid id);
    Task<BookingDto> CreateBookingAsync(BookingDto bookingDto);
    Task UpdateBookingAsync(BookingDto bookingDto);
    Task DeleteBookingAsync(Guid id);
}