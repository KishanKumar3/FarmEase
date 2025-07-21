using FarmEase.Domain.Entities;
using FarmEase.Infrastructure.Data;
using FarmEase.Infrastructure.Repository.Interface;

namespace FarmEase.Infrastructure.Repository.Implementation
{
    public class FarmRoomRepository : Repository<FarmRoom>, IFarmRoomRepository
    {
        private readonly ApplicationDBContext _db;

        public FarmRoomRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
        public void update(FarmRoom farmNumber)
        {
            _db.FarmRooms.Update(farmNumber);
        }
    }
}
