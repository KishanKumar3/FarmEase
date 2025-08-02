using FarmEase.Domain.DTO;

namespace FarmEase.Application.Services.Interface
{
    public interface IAccountService
    {
        public Task<string> Register(RegisterModel model);
        public Task<string> Login(LoginModel model);
    }
}
