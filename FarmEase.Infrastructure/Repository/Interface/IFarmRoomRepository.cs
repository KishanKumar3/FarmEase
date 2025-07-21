using FarmEase.Domain.Entities;

namespace FarmEase.Infrastructure.Repository.Interface
{
    public interface IFarmRoomRepository :  IRepository<FarmRoom>
    {
        void update(FarmRoom farmNumber);
    }
}
