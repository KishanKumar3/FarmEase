using FarmEase.Domain.Entities;

namespace FarmEase.Infrastructure.Repository.Interface
{
    public interface IBookingRepository : IRepository<Booking>
    {
        void update(Booking booking);
    }
}
