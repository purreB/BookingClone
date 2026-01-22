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
            var idx = _bookings.FindIndex(b => b.Id == booking.Id);
            if (idx >= 0) _bookings[idx] = booking;
        }
        public void Delete(Guid id) => _bookings.RemoveAll(b => b.Id == id);
    }
}