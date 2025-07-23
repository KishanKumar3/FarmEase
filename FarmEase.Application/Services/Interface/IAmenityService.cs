using FarmEase.Domain.DTO;
using FarmEase.Domain.Entities;

namespace FarmEase.Application.Services.Interface;

public interface IAmenityService
{
    Task<IEnumerable<Amenity>> GetAllAsync(int farmId);
    Task<string> Create(Amenity amenity);
    Task<string> Update(AmenityDTO amenityDto);
    Task<Amenity> GetByIdAsync(int id);
    Task<string> Delete(int id);
}
