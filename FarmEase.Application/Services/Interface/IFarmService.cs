using FarmEase.Domain.DTO;
using FarmEase.Domain.Entities;

namespace FarmEase.Application.Services.Interface;

public interface IFarmService
{
    Task<IEnumerable<Farm>> GetAllAsync();
    Task<Farm> GetByIdAsync(int id);
    Task<string> Create(FarmModel farm);
    Task<string> Update(FarmModel farm , int id);
    Task<string> Delete(int id);
    Task<IEnumerable<Farm>> GetFarmsAvailabilityByDate(DateTime checkInDate ,  DateTime checkOutDate);  
}