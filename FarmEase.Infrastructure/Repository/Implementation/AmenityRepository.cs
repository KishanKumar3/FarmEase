using FarmEase.Domain.Entities;
using FarmEase.Infrastructure.Data;
using FarmEase.Infrastructure.Repository.Interface;

namespace FarmEase.Infrastructure.Repository.Implementation
{
    public class AmenityRepository : Repository<Amenity>, IAmenityRepository
    {
        private readonly ApplicationDBContext _db;

        public AmenityRepository(ApplicationDBContext db): base(db) 
        {
            _db = db;
        }
        public void update(Amenity amenity)
        {
            _db.Amenities.Update(amenity);
        }
    }
}
