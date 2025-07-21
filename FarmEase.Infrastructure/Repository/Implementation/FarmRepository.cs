using FarmEase.Domain.Entities;
using FarmEase.Infrastructure.Data;
using FarmEase.Infrastructure.Repository.Interface;

namespace FarmEase.Infrastructure.Repository.Implementation
{
     public class FarmRepository : Repository<Farm>, IFarmRepository
    {
        private readonly ApplicationDBContext _db;

        public FarmRepository(ApplicationDBContext db) : base(db)
        {
            _db = db;
        }
        public void update(Farm farm)
        {
            _db.Farms.Update(farm);
        }
    }
}
