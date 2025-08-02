using FarmEase.Domain.Entities;

namespace FarmEase.Infrastructure.Repository.Interface
{
    public interface IFarmRepository : IRepository<Farm>
    {
        void update(Farm farm);
    }
}
