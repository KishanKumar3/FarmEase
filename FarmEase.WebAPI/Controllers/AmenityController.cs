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
    public class AmenityController : ControllerBase
    {
        private readonly ILogger<AmenityController> _logger;
        private readonly IAmenityService _amentiyService;
        public AmenityController(ILogger<AmenityController> logger, IAmenityService amentiyService)
        {
            _logger = logger;
            _amentiyService = amentiyService;
        }

        /// <summary>
        /// Creates a new amenity in the system.
        /// </summary>
        /// <param name="amenity">amenity details.</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<string>))]
        [HttpPost("Create", Name = "Create Amenity")]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] Amenity amenity)
        {
            _logger.LogInformation("AmenityController.Create: begin");
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

                var result = await _amentiyService.Create(amenity);

                response = new ApiResponse<string>(result, true, null!);
                _logger.LogInformation("AmenityController.Create: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AmenityController.Create: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AmenityController.Create: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AmenityController.Create: {ex.Message}");
                response = new ApiResponse<string>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Deletes a amenity from the system.
        /// </summary>
        /// <param name="id">ameity ID</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<string>))]
        [HttpPost("Delete/{id:int}", Name = "Delete Amenity")]
        public async Task<ActionResult<ApiResponse<string>>> Delete(int id)
        {
            _logger.LogInformation("AmenityController.Delete: begin");
            ApiResponse<string> response;
            try
            {
                var result = await _amentiyService.Delete(id);

                response = new ApiResponse<string>(result, true, null!);
                _logger.LogInformation("AmenityController.Delete: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AmenityController.Delete: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AmenityController.Delete: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AmenityController.Delete: {ex.Message}");
                response = new ApiResponse<string>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Updates an existing amenity in the system.
        /// </summary>
        /// <param name="amenity">amenity details.</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<string>))]
        [HttpPost("Update", Name = "Update Amenity")]
        public async Task<ActionResult<ApiResponse<string>>> Update([FromBody] AmenityDTO amenity)
        {
            _logger.LogInformation("AmenityController.Update: begin");
            ApiResponse<string> response;
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("AmenityController.Update: Validation failed");
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    var errorMessage = string.Join(Constants.Separator.Semicolon, errors);
                    response = new ApiResponse<string>(null!, false, new ApiError(errorMessage, Constants.ErrorCode.BadRequest));
                    return BadRequest(response);
                }

                var result = await _amentiyService.Update(amenity);

                response = new ApiResponse<string>(result, true, null!);
                _logger.LogInformation("AmenityController.Update: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AmenityController.Update: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AmenityController.Update: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AmenityController.Update: {ex.Message}");
                response = new ApiResponse<string>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Fetch amenity from the system using ID.
        /// </summary>
        /// <param name="id">ameity ID</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Amenity>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<Amenity>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<Amenity>))]
        [HttpPost("GetById/{id:int}", Name = "Get Amenity By ID")]
        public async Task<ActionResult<ApiResponse<Amenity>>> GetAmenityById(int id)
        {
            _logger.LogInformation("AmenityController.GetAmenityById: begin");
            ApiResponse<Amenity> response;
            try
            {
                var result = await _amentiyService.GetByIdAsync(id);

                response = new ApiResponse<Amenity>(result, true, null!);
                _logger.LogInformation("AmenityController.GetAmenityById: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<Amenity>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AmenityController.GetAmenityById: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<Amenity>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AmenityController.GetAmenityById: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AmenityController.GetAmenityById: {ex.Message}");
                response = new ApiResponse<Amenity>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Fetch all amenities from the system for a specific farm.
        /// </summary>
        /// <param name="farmId">ameity ID</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<Amenity>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<IEnumerable<Amenity>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<IEnumerable<Amenity>>))]
        [HttpPost("GetAll/{farmId:int}", Name = "Get All Amenities For A Farm")]
        public async Task<ActionResult<ApiResponse<IEnumerable<Amenity>>>> GetAll(int farmId)
        {
            _logger.LogInformation("AmenityController.GetAmenityById: GetAll");
            ApiResponse<IEnumerable<Amenity>> response;
            try
            {
                var result = await _amentiyService.GetAllAsync(farmId);

                response = new ApiResponse<IEnumerable<Amenity>>(result, true, null!);
                _logger.LogInformation("AmenityController.GetAll: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<IEnumerable<Amenity>>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AmenityController.GetAll: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<IEnumerable<Amenity>>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AmenityController.GetAll: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AmenityController.GetAll: {ex.Message}");
                response = new ApiResponse<IEnumerable<Amenity>>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
