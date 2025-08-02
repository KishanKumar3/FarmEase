using FarmEase.Domain.Entities;
using FarmEase.Infrastructure.Data;
using FarmEase.Infrastructure.Repository.Interface;

namespace FarmEase.Infrastructure.Repository.Implementation
{
    public class BookingRepository(ApplicationDBContext _db) : Repository<Booking>(_db), IBookingRepository
    {

        public void update(Booking booking)
        {
            _db.Bookings.Update(booking);
        }
    }
}

