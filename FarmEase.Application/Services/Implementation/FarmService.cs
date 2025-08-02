using AutoMapper;
using FarmEase.Application.Services.Interface;
using FarmEase.Domain.DTO;
using FarmEase.Domain.Entities;
using FarmEase.Domain.Enum;
using FarmEase.Domain.Helper;
using FarmEase.Infrastructure.Repository.UnitOfWork;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;

public class FarmService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FarmService> logger) : IFarmService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ILogger<FarmService> _logger = logger;

    public async Task<string> Create(FarmModel farm)
    {
        _logger.LogInformation($"{nameof(FarmService)}.{nameof(Create)}: begin");
        try
        {
            Farm farmEntity;
            if (farm.Image != null && farm.Image.Length > 0)
            {
                var fileExtension = Path.GetExtension(farm.Image.FileName);
                if (!Constants.ValidExtensions.Contains(fileExtension) || farm.Image.Length > 5 * 1024 * 1024)
                {
                    _logger.LogWarning($"{nameof(FarmService)}.{nameof(Create)}: Unsupported file - {farm.Image.FileName}");
                    throw new CustomException(Constants.ErrorMessages.InvalidFile);
                }

                var fileName = Guid.NewGuid() + fileExtension;
                var path = Path.Combine(Directory.GetCurrentDirectory(), @"images");
                var stream = new FileStream(Path.Combine(path, fileName), FileMode.Create);
                await farm.Image.CopyToAsync(stream);

                farmEntity = _mapper.Map<Farm>(farm);
                if (farmEntity == null)
                {
                    _logger.LogError($"{nameof(FarmService)}.{nameof(Create)}: Mapping failed");
                    throw new CustomException(String.Format(Constants.ErrorMessages.MappingError, nameof(farm), typeof(Farm)));
                }

                farmEntity.ImageUrl = @"\images\" + fileName;
                await _unitOfWork.Farm.Add(farmEntity);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                farmEntity = _mapper.Map<Farm>(farm);
                if (farmEntity == null)
                {
                    _logger.LogError($"{nameof(FarmService)}.{nameof(Create)}: Mapping failed");
                    throw new CustomException(String.Format(Constants.ErrorMessages.MappingError, nameof(farm), typeof(Farm)));
                }

                farmEntity.ImageUrl = Constants.PlaceHolderImage;
                farmEntity.Created_Date = DateTime.Now;
                await _unitOfWork.Farm.Add(farmEntity);
                await _unitOfWork.SaveChangesAsync();
            }

            _logger.LogInformation($"{nameof(FarmService)}.{nameof(Create)}: Success for Farm ID {farmEntity.Id}");
            return String.Format(Constants.SuccessMessages.Created, Constants.Entities.Farm, farmEntity.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(FarmService)}.{nameof(Create)}: {ex.Message}");
            throw new CustomException(ex.Message);
        }
    }

    public async Task<string> Delete(int id)
    {
        _logger.LogInformation($"{nameof(FarmService)}.{nameof(Delete)}: begin with id = {id}");
        try
        {
            var farm = await _unitOfWork.Farm.GetAsync(x => x.Id == id, tracked: true);
            if (farm == null)
            {
                _logger.LogWarning($"{nameof(FarmService)}.{nameof(Delete)}: No farm found with id = {id}");
                throw new CustomException(Constants.ErrorMessages.NoRecordFound);
            }

            farm.IsDelete = true;
            farm.IsAvailable = false;
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"{nameof(FarmService)}.{nameof(Delete)}: Deleted farm id = {farm.Id}");
            return String.Format(Constants.SuccessMessages.Deleted, Constants.Entities.Farm, farm.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(FarmService)}.{nameof(Delete)}: {ex.Message}");
            throw new CustomException(ex.Message);
        }
    }

    public async Task<IEnumerable<Farm>> GetAllAsync()
    {
        _logger.LogInformation($"{nameof(FarmService)}.{nameof(GetAllAsync)}: begin");
        try
        {
            var farms = await _unitOfWork.Farm.GetAllAsync(x => x.IsAvailable && !x.IsDelete);
            if (!farms.Any())
            {
                _logger.LogWarning($"{nameof(FarmService)}.{nameof(GetAllAsync)}: No farms found");
                throw new CustomException(Constants.ErrorMessages.NoRecordFound);
            }

            _logger.LogInformation($"{nameof(FarmService)}.{nameof(GetAllAsync)}: Fetched {farms.Count()} farms");
            return farms;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(FarmService)}.{nameof(GetAllAsync)}: {ex.Message}");
            throw new CustomException(ex.Message);
        }
    }

    public async Task<Farm> GetByIdAsync(int id)
    {
        _logger.LogInformation($"{nameof(FarmService)}.{nameof(GetByIdAsync)}: begin with id = {id}");
        try
        {
            var includeProps = new List<string> { Constants.Entities.Amenity, Constants.Entities.FarmRoom };
            var farm = await _unitOfWork.Farm.GetAsync(
                x => x.Id == id && x.IsAvailable && !x.IsDelete,
                includeProperties: string.Join(Constants.Separator.Comma, includeProps));

            if (farm == null)
            {
                _logger.LogWarning($"{nameof(FarmService)}.{nameof(GetByIdAsync)}: No farm found with id = {id}");
                throw new CustomException(Constants.ErrorMessages.NoRecordFound);
            }

            _logger.LogInformation($"{nameof(FarmService)}.{nameof(GetByIdAsync)}: Success");
            return farm;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(FarmService)}.{nameof(GetByIdAsync)}: {ex.Message}");
            throw new CustomException(ex.Message);
        }
    }

    public async Task<IEnumerable<Farm>> GetFarmsAvailabilityByDate(DateTime? checkInDate, DateTime? checkOutDate)
    {
        _logger.LogInformation($"{nameof(FarmService)}.{nameof(GetFarmsAvailabilityByDate)}: begin from {checkInDate} to {checkOutDate}");
        try
        {
            var includeProps = new List<string> { Constants.Entities.Amenity, Constants.Entities.FarmRoom };
            var farms = await _unitOfWork.Farm.GetAllAsync(
                x => x.IsAvailable && !x.IsDelete,
                includeProperties: string.Join(Constants.Separator.Comma, includeProps));

            if (!farms.Any())
            {
                _logger.LogWarning($"{nameof(FarmService)}.{nameof(GetFarmsAvailabilityByDate)}: No farms available");
                throw new CustomException(Constants.ErrorMessages.NoRecordFound);
            }

            var availableFarms = new List<Farm>();
            foreach (var farm in farms)
            {
                var isAvailable = await IsFarmAvailableByDate(farm.Id, checkInDate, checkOutDate);
                if (isAvailable) availableFarms.Add(farm);
            }

            if (!availableFarms.Any())
            {
                _logger.LogWarning($"{nameof(FarmService)}.{nameof(GetFarmsAvailabilityByDate)}: No farms available in given date range");
                throw new CustomException(Constants.ErrorMessages.NotAvailable);
            }

            _logger.LogInformation($"{nameof(FarmService)}.{nameof(GetFarmsAvailabilityByDate)}: {availableFarms.Count} farms available");
            return availableFarms;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(FarmService)}.{nameof(GetFarmsAvailabilityByDate)}: {ex.Message}");
            throw new CustomException(ex.Message);
        }
    }

    public async Task<string> Update(FarmModel farmModel, int id)
    {
        _logger.LogInformation($"{nameof(FarmService)}.{nameof(Update)}: begin for id = {id}");
        try
        {
            var farm = await _unitOfWork.Farm.GetAsync(x => x.Id == id, tracked: true);
            if (farm == null)
            {
                _logger.LogWarning($"{nameof(FarmService)}.{nameof(Update)}: Farm not found with id = {id}");
                throw new CustomException(Constants.ErrorMessages.NoRecordFound);
            }

            if (farm.Image != null)
            {
                string fileName = Guid.NewGuid() + Path.GetExtension(farm.Image.FileName);
                string imagePath = Path.Combine(Directory.GetCurrentDirectory(), @"images");

                if (!string.IsNullOrEmpty(farm.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), farm.ImageUrl.TrimStart('\\'));
                    if (File.Exists(oldImagePath)) File.Delete(oldImagePath);
                }

                using var fileStream = new FileStream(Path.Combine(imagePath, fileName), FileMode.Create);
                farm.Image.CopyTo(fileStream);
                farm.ImageUrl = @"\images\" + fileName;
            }

            farm.Updated_Date = DateTime.Now;
            farm.Name = farmModel.Name ?? farm.Name;
            farm.Description = farmModel.Description ?? farm.Description;
            farm.Price = farmModel.Price == 0 ? farm.Price : farmModel.Price;
            farm.Sqft = farmModel.Sqft == 0 ? farm.Sqft : farmModel.Sqft;
            farm.Occupancy = farmModel.Occupancy == 0 ? farm.Occupancy : farmModel.Occupancy;

            _unitOfWork.Farm.update(farm);
            await _unitOfWork.SaveChangesAsync();

            _logger.LogInformation($"{nameof(FarmService)}.{nameof(Update)}: Farm updated successfully with id = {farm.Id}");
            return String.Format(Constants.SuccessMessages.Updated, Constants.Entities.Farm, farm.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(FarmService)}.{nameof(Update)}: {ex.Message}");
            throw new CustomException(ex.Message);
        }
    }

    private async Task<bool> IsFarmAvailableByDate(int farmId, DateTime? checkInDate, DateTime? checkOutDate)
    {
        try
        {
            var bookedFarms = await _unitOfWork.Booking.GetAllAsync(x =>
                x.FarmId == farmId &&
                x.Status == Status.CONFIRMED &&
                (
                    (checkInDate >= x.CheckInDate && checkInDate <= x.CheckOutDate) ||
                    (checkOutDate >= x.CheckInDate && checkOutDate <= x.CheckOutDate) ||
                    (checkInDate <= x.CheckInDate && checkOutDate >= x.CheckOutDate)
                ));

            return !bookedFarms.Any();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{nameof(FarmService)}.{nameof(IsFarmAvailableByDate)}: {ex.Message}");
            throw new CustomException(ex.Message);
        }
    }
}
