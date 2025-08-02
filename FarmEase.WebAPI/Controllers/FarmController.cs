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
    public class FarmController : ControllerBase
    {
        private readonly ILogger<FarmController> _logger;
        private readonly IFarmService _farmService;
        public FarmController(ILogger<FarmController> logger, IFarmService farmService)
        {
            _logger = logger;
            _farmService = farmService;
        }

        /// <summary>
        /// Creates a new farm in the system.
        /// </summary>
        /// <param name="farm">farm details.</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<string>))]
        [HttpPost("Create", Name = "Create Farm")]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromForm] FarmModel farm)
        {
            _logger.LogInformation("FarmController.Create: begin");
            ApiResponse<string> response;
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("FarmController.Create: Validation failed");
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    var errorMessage = string.Join(Constants.Separator.Semicolon, errors);
                    response = new ApiResponse<string>(null!, false, new ApiError(errorMessage, Constants.ErrorCode.BadRequest));
                    return BadRequest(response);
                }

                var result = await _farmService.Create(farm);

                response = new ApiResponse<string>(result, true, null!);
                _logger.LogInformation("FarmController.Create: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmController.Create: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmController.Create: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"FarmController.Create: {ex.Message}");
                response = new ApiResponse<string>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Updates an existing farm in the system.
        /// </summary>
        /// <param name="farm">farm details.</param>
        /// <param name="id">farm ID.</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<string>))]
        [HttpPost("Update/{id:int}", Name = "Update Farm")]
        public async Task<ActionResult<ApiResponse<string>>> Update([FromForm] FarmModel farm, int id)
        {
            _logger.LogInformation("FarmController.Update: begin");
            ApiResponse<string> response;
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("FarmController.Create: Validation failed");
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    var errorMessage = string.Join(Constants.Separator.Semicolon, errors);
                    response = new ApiResponse<string>(null!, false, new ApiError(errorMessage, Constants.ErrorCode.BadRequest));
                    return BadRequest(response);
                }

                var result = await _farmService.Update(farm, id);

                response = new ApiResponse<string>(result, true, null!);
                _logger.LogInformation("FarmController.Update: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmController.Update: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmController.Update: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"FarmController.Update: {ex.Message}");
                response = new ApiResponse<string>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        
        /// <summary>
        /// Deletes an existing farm from the system.
        /// </summary>
        /// <param name="id">farm ID.</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<string>))]
        [HttpPost("Delete/{id:int}", Name = "Delete Farm")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            _logger.LogInformation("FarmController.Delete: begin");
            ApiResponse<string> response;
            try
            {
                var result = await _farmService.Delete(id);

                response = new ApiResponse<string>(result, true, null!);
                _logger.LogInformation("FarmController.Delete: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmController.Delete: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmController.Delete: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"FarmController.Delete: {ex.Message}");
                response = new ApiResponse<string>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        
        /// <summary>
        /// Fetches all existing farms from the system.
        /// </summary>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<Farm>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<IEnumerable<Farm>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<IEnumerable<Farm>>))]
        [HttpPost("GetAll", Name = "Get All Farms")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Farm>>>> GetAllFarms()
        {
            _logger.LogInformation("FarmController.GetAllFarms: begin");
            ApiResponse<IEnumerable<Farm>> response;
            try
            {
                var result = await _farmService.GetAllAsync();

                response = new ApiResponse<IEnumerable<Farm>>(result, true, null!);
                _logger.LogInformation("FarmController.GetAllFarms: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<IEnumerable<Farm>>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmController.GetAllFarms: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<IEnumerable<Farm>>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmController.GetAllFarms: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"FarmController.GetAllFarms: {ex.Message}");
                response = new ApiResponse<IEnumerable<Farm>>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Fetches an existing farm from the system using ID.
        /// </summary>
        /// <param name="id">farm ID.</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Farm>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<Farm>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<Farm>))]
        [HttpPost("GetById/{id:int}", Name = "Get Farm By Id")]
        public async Task<ActionResult<ApiResponse<Farm>>> GetFarmById(int id)
        {
            _logger.LogInformation("FarmController.GetFarmById: begin");
            ApiResponse<Farm> response;
            try
            {
                var result = await _farmService.GetByIdAsync(id);

                response = new ApiResponse<Farm>(result, true, null!);
                _logger.LogInformation("FarmController.GetFarmById: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<Farm>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmController.GetFarmById: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<Farm>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmController.GetFarmById: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"FarmController.GetFarmById: {ex.Message}");
                response = new ApiResponse<Farm>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        
        /// <summary>
        /// Get farms availability by date.
        /// </summary>
        /// <param name="checkInDate">Check-in date.</param>
        /// <param name="checkOutDate">Check-out date.</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<Farm>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<IEnumerable<Farm>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<IEnumerable<Farm>>))]
        [HttpPost("GetFarmsAvailabilityByDate", Name = "Get Farms Availability By Date")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Farm>>>> GetFarmsAvailabilityByDate([FromQuery(Name = Constants.QueryParameters.CheckInDate)] DateTime? checkInDate, [FromQuery(Name = Constants.QueryParameters.CheckOutDate)] DateTime? checkOutDate)
        {
            _logger.LogInformation("FarmController.GetFarmsAvailabilityByDate: begin");
            ApiResponse<IEnumerable<Farm>> response;
            try
            {
                if (!checkInDate.HasValue || !checkOutDate.HasValue)
                {
                    List<string> mandatoryParams = new List<string>{ Constants.QueryParameters.CheckInDate, Constants.QueryParameters.CheckOutDate };
                    throw new ArgumentException(String.Format(Constants.ErrorMessages.ValidationError, String.Join(Constants.Separator.Comma, mandatoryParams)));
                }
                var result = await _farmService.GetFarmsAvailabilityByDate(checkInDate, checkOutDate);

                response = new ApiResponse<IEnumerable<Farm>>(result, true, null!);
                _logger.LogInformation("FarmController.GetFarmsAvailabilityByDate: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<IEnumerable<Farm>>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmController.GetFarmsAvailabilityByDate: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<IEnumerable<Farm>>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmController.GetFarmsAvailabilityByDate: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"FarmController.GetFarmsAvailabilityByDate: {ex.Message}");
                response = new ApiResponse<IEnumerable<Farm>>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
