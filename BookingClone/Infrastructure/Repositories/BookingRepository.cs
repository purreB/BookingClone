using System.Collections.Generic;
using BookingClone.Domain;

namespace BookingClone.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly List<Booking> _bookings = new(); // Replace with EF Core DbContext

        public IEnumerable<Booking> GetAll() => _bookings;
        public Booking? GetById(Guid id) => _bookings.FirstOrDefault(b => b.Id == id);
        public void Add(Booking booking) => _bookings.Add(booking);
        public void Update(Booking booking)
        {
            // Update logic here
        }
        public void Delete(Guid id)
        {
            var booking = GetById(id);
            if (booking != null) _bookings.Remove(booking);
        }
    }
}