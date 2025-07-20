using FarmEase.Application.Services.Interface;
using FarmEase.Domain.DTO;
using FarmEase.Domain.Helper;
using Microsoft.AspNetCore.Mvc;
using Shared.Exceptions;

namespace FarmEase.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;
        public AccountController(ILogger<AccountController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="registerModel">The registration model containing user details.</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<string>))]
        [HttpPost("Register", Name = "Register user")]
        public async Task<ActionResult<ApiResponse<string>>> Register([FromBody] RegisterModel registerModel)
        {
            _logger.LogInformation("AccountController.Register: begin");
            ApiResponse<string> response;
            try
            {
                if(!ModelState.IsValid)
                {
                    _logger.LogWarning("AccountController.Register: Validation failed");
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    var errorMessage = string.Join("; ", errors);
                    response = new ApiResponse<string>(null!, false, new ApiError(errorMessage, Constants.ErrorCode.BadRequest));
                    return BadRequest(response);
                }

                _logger.LogInformation("AccountController.Register: user registration start");
                var result = await _accountService.Register(registerModel);
                if(result == Constants.ErrorMessages.RegistrationError)
                {
                    response = new ApiResponse<string>(null!, false, new ApiError(Constants.ErrorMessages.RegistrationError, Constants.ErrorCode.BadRequest));
                    return BadRequest(response);
                }
                response = new ApiResponse<string>(result, true, null!);
                _logger.LogInformation("AccountController.Register: end");
                return Ok(response);
            }
            catch(ArgumentException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AccountController.Register: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AccountController.Register: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AccountController.Register: {ex.Message}");
                response = new ApiResponse<string>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }

        /// <summary>
        /// Login to system.
        /// </summary>
        /// <param name="loginModel">The login model containing user email and password.</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<string>))]
        [HttpPost("Login", Name = "Login user")]
        public async Task<ActionResult<ApiResponse<string>>> Login([FromBody] LoginModel loginModel)
        {
            _logger.LogInformation("AccountController.Login: begin");
            ApiResponse<string> response;
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("AccountController.Login: Validation failed");
                    var errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                        .ToList();
                    var errorMessage = string.Join("; ", errors);
                    response = new ApiResponse<string>(null!, false, new ApiError(errorMessage, Constants.ErrorCode.BadRequest));
                    return BadRequest(response);
                }

                _logger.LogInformation("AccountController.Login: user registration start");
                var result = await _accountService.Login(loginModel);
                response = new ApiResponse<string>(result, true, null!);
                _logger.LogInformation("AccountController.Register: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AccountController.Register: {ex.Message}");
                return BadRequest(response);
            }
            catch(CustomException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"AccountController.Register: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AccountController.Register: {ex.Message}");
                response = new ApiResponse<string>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
