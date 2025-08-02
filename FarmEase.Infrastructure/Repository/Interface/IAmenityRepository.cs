using FarmEase.Domain.Entities;

namespace FarmEase.Infrastructure.Repository.Interface
{
    public interface IAmenityRepository : IRepository<Amenity>
    {
        void update(Amenity amenity);
    }
}
