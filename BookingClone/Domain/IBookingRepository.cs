using System.Collections.Generic;
using BookingClone.Domain;

namespace BookingClone.Infrastructure.Repositories
{
    public interface IBookingRepository
    {
        IEnumerable<Booking> GetAll();
        Booking? GetById(Guid id);
        void Add(Booking booking);
        void Update(Booking booking);
        void Delete(Guid id);
    }
}