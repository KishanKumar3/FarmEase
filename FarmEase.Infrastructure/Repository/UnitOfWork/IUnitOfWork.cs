using FarmEase.Infrastructure.Repository.Interface;

namespace FarmEase.Infrastructure.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        IFarmRoomRepository FarmRoom{ get; set; }
        IFarmRepository Farm { get; set; }
        IApplicationUserRepository ApplicationUser { get; set; }
        IAmenityRepository Amenity { get; set; }
        IBookingRepository Booking { get; set; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}
