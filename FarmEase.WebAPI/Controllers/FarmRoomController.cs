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
    public class FarmRoomController : ControllerBase
    {
        private readonly ILogger<FarmRoomController> _logger;
        private readonly IFarmRoomService _farmRoomService;
        public FarmRoomController(ILogger<FarmRoomController> logger, IFarmRoomService farmRoomService)
        {
            _logger = logger;
            _farmRoomService = farmRoomService;
        }

        /// <summary>
        /// Creates a new farm room in the system.
        /// </summary>
        /// <param name="farmRoom">farm details.</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<string>))]
        [HttpPost("Create", Name = "Create Farm Room")]
        public async Task<ActionResult<ApiResponse<string>>> Create([FromBody] FarmRoom farmRoom)
        {
            _logger.LogInformation("FarmRoomController.Create: begin");
            ApiResponse<string> response;
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("FarmRoomController.Create: Validation failed");
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    var errorMessage = string.Join(Constants.Separator.Semicolon, errors);
                    response = new ApiResponse<string>(null!, false, new ApiError(errorMessage, Constants.ErrorCode.BadRequest));
                    return BadRequest(response);
                }

                var result = await _farmRoomService.CreateAsync(farmRoom);

                response = new ApiResponse<string>(result, true, null!);
                _logger.LogInformation("FarmRoomController.Create: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmRoomController.Create: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmRoomController.Create: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"FarmRoomController.Create: {ex.Message}");
                response = new ApiResponse<string>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        
        /// <summary>
        /// Updates an existing farm room in the system.
        /// </summary>
        /// <param name="farmRoom">farm details.</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<string>))]
        [HttpPost("Update", Name = "Update Farm Room")]
        public async Task<ActionResult<ApiResponse<string>>> Update([FromBody] FarmRoom farmRoom)
        {
            _logger.LogInformation("FarmRoomController.Update: begin");
            ApiResponse<string> response;
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("FarmRoomController.Update: Validation failed");
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    var errorMessage = string.Join(Constants.Separator.Semicolon, errors);
                    response = new ApiResponse<string>(null!, false, new ApiError(errorMessage, Constants.ErrorCode.BadRequest));
                    return BadRequest(response);
                }

                var result = await _farmRoomService.UpdateAsync(farmRoom);

                response = new ApiResponse<string>(result, true, null!);
                _logger.LogInformation("FarmRoomController.Update: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmRoomController.Update: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmRoomController.Update: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"FarmRoomController.Update: {ex.Message}");
                response = new ApiResponse<string>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        
        /// <summary>
        /// Fetches farm room using ID.
        /// </summary>
        /// <param name="id">farm details.</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<FarmRoom>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<FarmRoom>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<FarmRoom>))]
        [HttpPost("GetById/{id:int}", Name = "Get Farm Room By Id")]
        public async Task<ActionResult<ApiResponse<FarmRoom>>> GetFarmRoomById(int id)
        {
            _logger.LogInformation("FarmRoomController.GetFarmRoomById: begin");
            ApiResponse<FarmRoom> response;
            try
            {
                var result = await _farmRoomService.GetByIdAsync(id);

                response = new ApiResponse<FarmRoom>(result, true, null!);
                _logger.LogInformation("FarmRoomController.GetFarmRoomById: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<FarmRoom>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmRoomController.GetFarmRoomById: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<FarmRoom>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmRoomController.GetFarmRoomById: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"FarmRoomController.GetFarmRoomById: {ex.Message}");
                response = new ApiResponse<FarmRoom>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
        
        /// <summary>
        /// Fetches all farm rooms for a farm.
        /// </summary>
        /// <param name="farmId">farm details.</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<IEnumerable<FarmRoom>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<IEnumerable<FarmRoom>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<IEnumerable<FarmRoom>>))]
        [HttpPost("GetAll/{farmId:int}", Name = "Get All Farm Room")]
        public async Task<ActionResult<ApiResponse<IEnumerable<FarmRoom>>>> GetAllFarmRoom(int farmId)
        {
            _logger.LogInformation("FarmRoomController.GetAllFarmRoom: begin");
            ApiResponse<IEnumerable<FarmRoom>> response;
            try
            {
                var result = await _farmRoomService.GetAllAsync(farmId);

                response = new ApiResponse<IEnumerable<FarmRoom>>(result, true, null!);
                _logger.LogInformation("FarmRoomController.GetAllFarmRoom: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<IEnumerable<FarmRoom>>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmRoomController.GetAllFarmRoom: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<IEnumerable<FarmRoom>>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"FarmRoomController.GetAllFarmRoom: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"FarmRoomController.GetAllFarmRoom: {ex.Message}");
                response = new ApiResponse<IEnumerable<FarmRoom>>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
