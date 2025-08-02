using FarmEase.Application.Services.Interface;
using FarmEase.Domain.DTO;
using FarmEase.Domain.Entities;
using FarmEase.Domain.Helper;
using FarmEase.Infrastructure.Repository.UnitOfWork;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;

namespace FarmEase.Application.Services.Implementation;

public class AmenityService : IAmenityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<AmenityService> _logger;
    public AmenityService(IUnitOfWork unitOfWork,  ILogger<AmenityService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<string> Create(Amenity amenity)
    {
        _logger.LogInformation($"{nameof(AmenityService)}.{nameof(Create)}: begin");
        try
        {
            var farm = await _unitOfWork.Farm.GetAsync(x => x.Id == amenity.FarmId);
            if (farm == null)
            {
                throw new CustomException(String.Format(Constants.ErrorMessages.NotExists, Constants.Entities.Farm, amenity.FarmId));
            }
            await _unitOfWork.Amenity.Add(amenity);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"{nameof(AmenityService)}.{nameof(Create)}: amenity created successfully");
            return String.Format(Constants.SuccessMessages.Created, Constants.Entities.Amenity, amenity.Id);
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, $"{nameof(AmenityService)}.{nameof(Create)}:{ex.Message}");
            throw new CustomException(ex.Message);
        }
    }

    public async Task<string> Delete(int id)
    {
        _logger.LogInformation($"{nameof(AmenityService)}.{nameof(Delete)}: begin");
        try
        {
            _logger.LogInformation($"{nameof(AmenityService)}.{nameof(Delete)}: fetching amenity with id - {id}");
            Amenity? amenity = await _unitOfWork.Amenity.GetAsync(a => a.Id == id, tracked: true);
            if(amenity == null)
            {
                _logger.LogInformation($"{nameof(AmenityService)}.{nameof(Delete)}: no amenity found with id - {id}");
                throw new CustomException(Constants.ErrorMessages.NoRecordFound);
            }
            amenity.IsDelete = true;
            amenity.IsAvailable = false;
            _logger.LogInformation($"{nameof(AmenityService)}.{nameof(Delete)}: amenity with id - {id} deleted successfully");
            await _unitOfWork.SaveChangesAsync();
            return String.Format(Constants.SuccessMessages.Deleted, Constants.Entities.Amenity, amenity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(AmenityService)}.{nameof(Delete)}:{ex.Message}");
            throw new CustomException(ex.Message);
        }
    }

    public async Task<IEnumerable<Amenity>> GetAllAsync(int farmId)
    {
        _logger.LogInformation($"{nameof(AmenityService)}.{nameof(GetAllAsync)}: begin");
        try
        {
            _logger.LogInformation($"{nameof(AmenityService)}.{nameof(GetAllAsync)}: Fetching amenities for Farm with id - {farmId}");
            var amenities = await _unitOfWork.Amenity.GetAllAsync(x => x.IsAvailable == true && x.FarmId == farmId);
            if (!amenities.Any())
            {
                _logger.LogInformation($"{nameof(AmenityService)}.{nameof(GetAllAsync)}: no amenities found for Farm with id - {farmId}");
                throw new CustomException(Constants.ErrorMessages.NoRecordFound);
            }
            _logger.LogInformation($"{nameof(AmenityService)}.{nameof(GetAllAsync)}: end");
            return amenities;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(AmenityService)}.{nameof(GetAllAsync)}:{ex.Message}");
            throw new CustomException(ex.Message);
        }
    }

    public async Task<Amenity> GetByIdAsync(int id)
    {
        _logger.LogInformation($"{nameof(AmenityService)}.{nameof(GetByIdAsync)}: begin");
        try
        {
            _logger.LogInformation($"{nameof(AmenityService)}.{nameof(GetByIdAsync)}: Fetching amenity with id - {id}");
            var amenity = await _unitOfWork.Amenity.GetAsync(x => x.Id == id && !x.IsDelete);
            if (amenity == null)
            {
                _logger.LogInformation($"{nameof(AmenityService)}.{nameof(GetByIdAsync)}: no amenity found with id - {id}");
                throw new CustomException(Constants.ErrorMessages.NoRecordFound);
            }
            _logger.LogInformation($"{nameof(AmenityService)}.{nameof(GetByIdAsync)}: end");
            return amenity;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(AmenityService)}.{nameof(GetByIdAsync)}:{ex.Message}");
            throw new CustomException(ex.Message);
        }
    }

    public async Task<string> Update(AmenityDTO amenityDto)
    {
        _logger.LogInformation($"{nameof(AmenityService)}.{nameof(Update)}: begin");
        try
        {
            _logger.LogInformation($"{nameof(AmenityService)}.{nameof(Update)}: Fetching amenity with id - {amenityDto.Id}");
            var amenity = await _unitOfWork.Amenity.GetAsync(x => x.Id == amenityDto.Id && !x.IsDelete);
            if (amenity == null)
            {
                _logger.LogInformation($"{nameof(AmenityService)}.{nameof(Update)}: no amenity found with id - {amenityDto.Id}");
                throw new CustomException(Constants.ErrorMessages.NoRecordFound);
            }
            amenity.Updated_Date = DateTime.Now;
            amenity.Name = amenityDto.Name ?? amenity.Name;
            amenity.Description = amenityDto.Description ?? amenity.Description;
            _unitOfWork.Amenity.update(amenity);
            await _unitOfWork.SaveChangesAsync();
            _logger.LogInformation($"{nameof(AmenityService)}.{nameof(Update)}: end");
            return String.Format(Constants.SuccessMessages.Updated, Constants.Entities.Amenity, amenity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(AmenityService)}.{nameof(Update)}:{ex.Message}");
            throw new CustomException(ex.Message);
        }
    }
}
