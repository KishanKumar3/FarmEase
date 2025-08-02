using AutoMapper;
using FarmEase.Application.Services.Interface;
using FarmEase.Domain.Entities;
using FarmEase.Domain.Helper;
using FarmEase.Infrastructure.Repository.UnitOfWork;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;

namespace FarmEase.Application.Services.Implementation
{
    public class FarmRoomService : IFarmRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<FarmRoomService> _logger;
        
        public FarmRoomService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<FarmRoomService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<string> CreateAsync(FarmRoom farmRoom)
        {
            _logger.LogInformation($"{nameof(FarmRoomService)}.{nameof(CreateAsync)}: begin");
            try
            {
                await _unitOfWork.FarmRoom.Add(farmRoom);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"{nameof(FarmRoomService)}.{nameof(CreateAsync)}: FarmRoom #{farmRoom.FarmRoom_Number} created for FarmId {farmRoom.FarmId}");
                return String.Format(Constants.SuccessMessages.Created, Constants.Entities.FarmRoom, farmRoom.FarmRoom_Number);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(FarmRoomService)}.{nameof(CreateAsync)}: {ex.Message}");
                throw new CustomException(ex.Message);
            }
        }

        public async Task<IEnumerable<FarmRoom>> GetAllAsync(int farmId)
        {
            _logger.LogInformation($"{nameof(FarmRoomService)}.{nameof(GetAllAsync)}: Retrieving farm rooms for FarmId {farmId}");
            try
            {
                var farmRooms = await _unitOfWork.FarmRoom.GetAllAsync(x => x.FarmId ==  farmId);
                if(!farmRooms.Any())
                {
                    throw new CustomException(Constants.ErrorMessages.NoRecordFound);
                }
                return farmRooms;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(FarmRoomService)}.{nameof(GetAllAsync)}: {ex.Message}");
                throw new CustomException(ex.Message);
            }
        }

        public async Task<FarmRoom> GetByIdAsync(int id)
        {
            _logger.LogInformation($"{nameof(FarmRoomService)}.{nameof(GetByIdAsync)}: Getting FarmRoom by ID: {id}");
            try
            {
                var farmRoom = await _unitOfWork.FarmRoom.GetAsync(x => x.FarmRoom_Number == id);
                if(farmRoom == null)
                {
                    _logger.LogWarning($"{nameof(FarmRoomService)}.{nameof(GetByIdAsync)}: No FarmRoom found with ID: {id}");
                    throw new CustomException(Constants.ErrorMessages.NoRecordFound);
                }
                return farmRoom;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(FarmRoomService)}.{nameof(GetByIdAsync)}: {ex.Message}");
                throw new CustomException(ex.Message);
            }
        }

        public async Task<string> UpdateAsync(FarmRoom farmRoom)
        {
            _logger.LogInformation($"{nameof(FarmRoomService)}.{nameof(UpdateAsync)}: begin");
            try
            {
                _unitOfWork.FarmRoom.update(farmRoom);
                await _unitOfWork.SaveChangesAsync();

                _logger.LogInformation($"{nameof(FarmRoomService)}.{nameof(UpdateAsync)}: FarmRoom #{farmRoom.FarmRoom_Number} updated successfully");
                return String.Format(Constants.SuccessMessages.Updated, Constants.Entities.FarmRoom, farmRoom.FarmRoom_Number);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(FarmRoomService)}.{nameof(UpdateAsync)}: {ex.Message}");
                throw new CustomException(ex.Message);
            }
        }
    }
}
