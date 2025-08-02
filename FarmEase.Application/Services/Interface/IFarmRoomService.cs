using FarmEase.Domain.Entities;

namespace FarmEase.Application.Services.Interface
{
    public interface IFarmRoomService
    {
        Task<IEnumerable<FarmRoom>> GetAllAsync(int farmId);
        Task<FarmRoom> GetByIdAsync(int id);
        Task<string> CreateAsync(FarmRoom farmRoom);
        Task<string> UpdateAsync(FarmRoom farmRoom);
    }
}
