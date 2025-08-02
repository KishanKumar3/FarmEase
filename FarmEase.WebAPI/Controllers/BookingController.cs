using FarmEase.Application.Services.Interface;
using FarmEase.Domain.DTO;
using FarmEase.Domain.Entities;
using FarmEase.Domain.Helper;
using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;

namespace FarmEase.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingService _bookingService;
        public BookingController(ILogger<BookingController> logger, IBookingService bookingService)
        {
            _logger = logger;
            _bookingService = bookingService;
        }

        /// <summary>
        /// Fetches booking details.
        /// </summary>
        /// <param name="farmId">Farm ID</param>
        /// <param name="checkInDate">Check-in date</param>
        /// <param name="nights">No. of nights</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<BookingDetails>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<BookingDetails>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<BookingDetails>))]
        [HttpPost("Details", Name = "Booking Details")]
        public async Task<ActionResult<ApiResponse<BookingDetails>>> BookingDetails([FromQuery] int? farmId, [FromQuery] DateTime? checkInDate, [FromQuery] int? nights)
        {
            _logger.LogInformation("BookingController.BookingDetails: begin");
            ApiResponse<BookingDetails> response;
            try
            {
                if (farmId == null || !checkInDate.HasValue || nights == null)
                {
                    List<string> mandatoryParams = new List<string> { Constants.QueryParameters.FarmId, Constants.QueryParameters.CheckInDate, Constants.QueryParameters.Nights };
                    throw new ArgumentException(String.Format(Constants.ErrorMessages.ValidationError, String.Join(Constants.Separator.Comma, mandatoryParams)));
                }

                var result = await _bookingService.GetBookingDetails((int)farmId, (DateTime)checkInDate, (int)nights);

                response = new ApiResponse<BookingDetails>(result, true, null!);
                _logger.LogInformation("BookingController.BookingDetails: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<BookingDetails>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"BookingController.BookingDetails: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<BookingDetails>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"BookingController.BookingDetails: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"BookingController.BookingDetails: {ex.Message}");
                response = new ApiResponse<BookingDetails>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        
        
        /// <summary>
        /// Create a booking.
        /// </summary>
        /// <param name="booking">Booking Info</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<string>))]
        [HttpPost("Create", Name = "Create Booking")]
        public async Task<ActionResult<ApiResponse<string>>> CreateBooking([FromBody] BookingModel booking)
        {
            _logger.LogInformation("BookingController.CreateBooking: begin");
            ApiResponse<string> response;
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("AmenityController.Create: Validation failed");
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    var errorMessage = string.Join(Constants.Separator.Semicolon, errors);
                    response = new ApiResponse<string>(null!, false, new ApiError(errorMessage, Constants.ErrorCode.BadRequest));
                    return BadRequest(response);
                }

                var result = await _bookingService.CreateBooking(booking);

                response = new ApiResponse<string>(result, true, null!);
                _logger.LogInformation("BookingController.CreateBooking: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"BookingController.CreateBooking: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"BookingController.CreateBooking: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"BookingController.CreateBooking: {ex.Message}");
                response = new ApiResponse<string>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        
        /// <summary>
        /// Fetches all bookings of a user.
        /// </summary>
        /// <param name="userId">User ID</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<Booking>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<IEnumerable<Booking>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<IEnumerable<Booking>>))]
        [HttpPost("GetAll", Name = "Get All Boookings")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Booking>>>> GetAllBookings([FromQuery] string userId)
        {
            _logger.LogInformation("BookingController.GetAllBookings: begin");
            ApiResponse<IEnumerable<Booking>> response;
            try
            {
                var result = await _bookingService.GetAllBookings(userId);

                response = new ApiResponse<IEnumerable<Booking>>(result, true, null!);
                _logger.LogInformation("BookingController.GetAllBookings: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<IEnumerable<Booking>>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"BookingController.GetAllBookings: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<IEnumerable<Booking>>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"BookingController.GetAllBookings: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"BookingController.GetAllBookings: {ex.Message}");
                response = new ApiResponse<IEnumerable<Booking>>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        
        /// <summary>
        /// Fetches booking using the bookingID.
        /// </summary>
        /// <param name="bookingId">Booking ID</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Booking>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<Booking>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<Booking>))]
        [HttpPost("GetById/{bookingId:int}", Name = "Get Boooking By Id")]
        public async Task<ActionResult<ApiResponse<Booking>>> GetBookingById(int bookingId)
        {
            _logger.LogInformation("BookingController.GetBookingById: begin");
            ApiResponse<Booking> response;
            try
            {
                var result = await _bookingService.GetBookingById(bookingId);

                response = new ApiResponse<Booking>(result, true, null!);
                _logger.LogInformation("BookingController.GetBookingById: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<Booking>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"BookingController.GetBookingById: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<Booking>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"BookingController.GetBookingById: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"BookingController.GetBookingById: {ex.Message}");
                response = new ApiResponse<Booking>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
