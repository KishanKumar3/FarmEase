using AutoMapper;
using FarmEase.Application.Services.Interface;
using FarmEase.Domain.DTO;
using FarmEase.Domain.Entities;
using FarmEase.Domain.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Shared.Exceptions;
using System.IdentityModel.Tokens.Jwt;

namespace FarmEase.Application.Services.Implementation
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IJWTService _jwtService;
        private readonly ILogger<AccountService> _logger;
        public AccountService(UserManager<ApplicationUser> userManager, IMapper mapper, IJWTService jWTService, ILogger<AccountService> logger)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtService = jWTService;
            _logger = logger;
        }

        public async Task<string> Login(LoginModel model)
        {
            _logger.LogInformation("AccountService.Login: begin");
            try
            {
                string jwtToken = string.Empty;
                var user = await _userManager.FindByEmailAsync(model.Email); 
                if(user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                    _logger.LogInformation("AccountService.Login: generating JWT token");
                    var token = await _jwtService.CreateJwtToken(user);
                    jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                }
                else
                {
                    _logger.LogError("AccountService.Login: invalid login");
                    throw new CustomException(Constants.ErrorMessages.InvalidLogin);
                }
                _logger.LogInformation("AccountService.Login: end");
                return jwtToken;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"AccountService.Login:{ex.Message}");
                throw new CustomException(ex.Message);
            }
        }

        public async Task<string> Register(RegisterModel model)
        {
            _logger.LogInformation("AccountService.Register: begin");
            try
            {
                string jwtToken = string.Empty;
                if(await _userManager.FindByEmailAsync(model.Email) is not null)
                {
                    _logger.LogError("AccountService.Register: user already exists");
                    throw new CustomException(Constants.ErrorMessages.UserExists);
                }
                var user = _mapper.Map<ApplicationUser>(model);
                _logger.LogInformation("AccountService.Register: creating user");
                var createUser = await _userManager.CreateAsync(user, model.Password);

                if(createUser.Succeeded)
                {
                    _logger.LogInformation("AccountService.Register: user created successfully");
                    await _userManager.AddToRoleAsync(user, model.Role);
                    var token = await _jwtService.CreateJwtToken(user);
                    jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                }
                else
                {
                    var errorDetails = string.Join("; ", createUser.Errors.Select(e => $"{e.Code}: {e.Description}"));
                    _logger.LogError($"AccountService.Register: Error while creating user - {errorDetails}");
                    throw new CustomException(Constants.ErrorMessages.RegistrationError);
                }
                _logger.LogInformation("AccountService.Register: end");
                return jwtToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"AccountService.Register:{ex.Message}");
                throw new CustomException(ex.Message);
            }
        }
    }
}
