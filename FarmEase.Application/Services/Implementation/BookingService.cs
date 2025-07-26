using AutoMapper;
using FarmEase.Application.Services.Interface;
using FarmEase.Domain.DTO;
using FarmEase.Domain.Entities;
using FarmEase.Domain.Enum;
using FarmEase.Domain.Helper;
using FarmEase.Infrastructure.Repository.UnitOfWork;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;

namespace FarmEase.Application.Services.Implementation
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFarmService _farmService;
        private readonly IEmailService _emailService;
        private readonly ILogger<BookingService> _logger;
        private readonly IMapper _mapper;
        public BookingService(IUnitOfWork unitOfWork, ILogger<BookingService> logger, IFarmService farmService, IEmailService emailService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _farmService = farmService;
            _emailService = emailService;
            _mapper = mapper;
        }
        public async Task<string> CreatBooking(BookingModel bookingModel)
        {
            _logger.LogInformation($"{nameof(BookingService)}.{nameof(CreatBooking)}: begin");
            try
            {
                _logger.LogInformation($"{nameof(BookingService)}.{nameof(CreatBooking)}: Mapped booking model to booking entity with farmId {bookingModel.FarmId}");
                var booking = _mapper.Map<Booking>(bookingModel);
                if(booking == null)
                {
                    throw new CustomException(String.Format(Constants.ErrorMessages.MappingError, typeof(BookingModel), typeof(Booking)));
                }
                booking.Status = Status.CONFIRMED;
                await _unitOfWork.Booking.Add(booking);
                await _unitOfWork.SaveChangesAsync();
                var htmlBody = string.Format(
                    Constants.BookingInfo.BookingConfirmationMessage,
                    booking.Name,
                    booking.Id,
                    booking.CheckInDate.ToString("dd-MM-yyyy"),
                    booking.CheckOutDate.ToString("dd-MM-yyyy"),
                    booking.Farm.Name,
                    booking.BookingDate.ToString("dd-MM-yyyy"),
                    booking.Nights,
                    booking.Farm.Price * booking.Nights
                );

                await _emailService.SendEmailAsync(booking.Email, Constants.BookingInfo.Subject, htmlBody);
                _logger.LogInformation($"{nameof(BookingService)}.{nameof(CreatBooking)}: booking created successfully");
                return String.Format(Constants.SuccessMessages.Created, Constants.Entities.Booking, booking.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BookingService)}.{nameof(CreatBooking)}:{ex.Message}");
                throw new CustomException(ex.Message);
            }
        }

        public async Task<IEnumerable<Booking>> GetAllBookings(string? userId)
        {
            _logger.LogInformation($"{nameof(BookingService)}.{nameof(GetAllBookings)}: begin");
            try
            {
                IEnumerable<Booking> bookings;
                var includeProps = new List<string>() { 
                    Constants.DbSet.ApplicationUsers,
                    Constants.DbSet.Farms,
                };

                bookings = !string.IsNullOrEmpty(userId) ? await _unitOfWork.Booking.GetAllAsync(x => x.UserId == userId, includeProperties: string.Join(Constants.Separator.Comma, includeProps)) : await _unitOfWork.Booking.GetAllAsync(includeProperties: string.Join(Constants.Separator.Comma, includeProps));
                if(!bookings.Any())
                {
                    throw new CustomException(Constants.ErrorMessages.NoRecordFound);
                }
                _logger.LogInformation($"{nameof(BookingService)}.{nameof(GetAllBookings)}: Fetched {bookings.Count()} bookings");
                return bookings;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BookingService)}.{nameof(GetAllBookings)}:{ex.Message}");
                throw new CustomException(ex.Message);
            }
        }

        public async Task<Booking> GetBookingById(int bookingId)
        {
            _logger.LogInformation($"{nameof(BookingService)}.{nameof(GetBookingById)}: begin");
            try
            {
                var booking = await _unitOfWork.Booking.GetAsync(x => x.Id == bookingId);
                if (booking == null)
                {
                    throw new CustomException(Constants.ErrorMessages.NoRecordFound);
                }
                return booking;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BookingService)}.{nameof(GetBookingById)}:{ex.Message}");
                throw new CustomException(ex.Message);
            }
        }

        public async Task<BookingDetails> GetBookingDetails(int farmId, DateTime checkInDate, int nights)
        {
            _logger.LogInformation($"{nameof(BookingService)}.{nameof(GetBookingDetails)}: begin");
            try
            {
                var farm = await _farmService.GetByIdAsync(farmId);
                var farmModel = _mapper.Map<FarmModel>(farm);
                if (farmModel == null)
                {
                    throw new CustomException(String.Format(Constants.ErrorMessages.MappingError, typeof(Farm), typeof(FarmModel)));
                }
                var bookingDetails = new BookingDetails()
                {
                    farm = farmModel,
                    CheckInDate = checkInDate,
                    nights = nights,
                    TotalCost = farmModel.Price * nights
                };
                return bookingDetails;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{nameof(BookingService)}.{nameof(GetBookingDetails)}:{ex.Message}");
                throw new CustomException(ex.Message);
            }
        }
    }
}
