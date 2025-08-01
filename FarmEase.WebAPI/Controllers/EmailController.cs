using AutoMapper.Internal;
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
    public class EmailController : ControllerBase
    {
        private readonly ILogger<EmailController> _logger;
        private readonly IEmailService _emailService;
        public EmailController(ILogger<EmailController> logger, IEmailService emailService)
        {
            _logger = logger;
            _emailService = emailService;
        }

        /// <summary>
        /// sends email
        /// </summary>
        /// <param name="mailRequest">email details</param>
        /// <returns>Returns success status or appropriate error messages.</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiResponse<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<string>))]
        [HttpPost("SendEmail", Name = "Send Email")]
        public async Task<ActionResult<ApiResponse<string>>> SendEmail([FromForm] MailRequestModel mailRequest)
        {
            _logger.LogInformation("EmailController.SendEmail: begin");
            ApiResponse<string> response;
            try
            {
                var result = await _emailService.SendEmailAsync(mailRequest.ToEmail, mailRequest.Subject, mailRequest.Message);

                response = new ApiResponse<string>(result, true, null!);
                _logger.LogInformation("EmailController.SendEmail: end");
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"EmailController.SendEmail: {ex.Message}");
                return BadRequest(response);
            }
            catch (CustomException ex)
            {
                response = new ApiResponse<string>(new ApiError(ex.Message, Constants.ErrorCode.BadRequest));
                _logger.LogError(ex, $"EmailController.SendEmail: {ex.Message}");
                return BadRequest(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"EmailController.SendEmail: {ex.Message}");
                response = new ApiResponse<string>(null!, false,
                    new ApiError(Constants.ErrorMessages.UnexpectedError, Constants.ErrorCode.Problem));
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
