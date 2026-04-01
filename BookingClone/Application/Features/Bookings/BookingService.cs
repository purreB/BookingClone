using AutoMapper;
using BookingClone.Application.DTOs;
using BookingClone.Domain.Bookings;
using BookingClone.Domain.Bookings.Repositories;

namespace BookingClone.Application.Features.Bookings;

public class BookingService(IBookingRepository bookingRepository, IMapper mapper) : IBookingService
{
    public async Task<IEnumerable<BookingDto>> GetAllBookingsAsync()
    {
        var bookings = await bookingRepository.GetAllAsync();
        return mapper.Map<IEnumerable<BookingDto>>(bookings);
    }

    public async Task<BookingDto?> GetBookingByIdAsync(Guid id)
    {
        var booking = await bookingRepository.GetByIdAsync(id);
        return booking == null ? null : mapper.Map<BookingDto>(booking);
    }

    public async Task<BookingDto> CreateBookingAsync(BookingDto bookingDto)
    {
        var booking = mapper.Map<Booking>(bookingDto);
        booking.Id = Guid.NewGuid();
        booking.CreatedAt = DateTime.UtcNow;
        booking.Status = BookingStatus.Pending;

        await bookingRepository.AddAsync(booking);

        return mapper.Map<BookingDto>(booking);
    }

    public async Task UpdateBookingAsync(BookingDto bookingDto)
    {
        var booking = mapper.Map<Booking>(bookingDto);
        await bookingRepository.UpdateAsync(booking);
    }

    public async Task DeleteBookingAsync(Guid id)
    {
        await bookingRepository.DeleteAsync(id);
    }
}
