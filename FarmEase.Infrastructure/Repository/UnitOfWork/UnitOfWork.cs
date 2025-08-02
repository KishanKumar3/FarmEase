using FarmEase.Infrastructure.Data;
using FarmEase.Infrastructure.Repository.Implementation;
using FarmEase.Infrastructure.Repository.Interface;

namespace FarmEase.Infrastructure.Repository.UnitOfWork
{
    public class UnitOfWork(ApplicationDBContext applicationDbContext) : IUnitOfWork
    {
        public IFarmRoomRepository FarmRoom{ get; set; } = new FarmRoomRepository(applicationDbContext);
        public IFarmRepository Farm { get; set; } = new FarmRepository(applicationDbContext);
        public IApplicationUserRepository ApplicationUser { get; set; } = new ApplicationUserRepository(applicationDbContext);
        public IAmenityRepository Amenity { get; set; } = new AmenityRepository(applicationDbContext);
        public IBookingRepository Booking { get; set; } = new BookingRepository(applicationDbContext);

        public void SaveChanges()
        {
            applicationDbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
           await applicationDbContext.SaveChangesAsync();
        }
    }
}
