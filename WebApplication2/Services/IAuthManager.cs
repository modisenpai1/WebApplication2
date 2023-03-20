using WebApplication2.Domain.DTOs;

namespace WebApplication2.Services
{
    public interface IAuthManager
    {
        public Task<bool> AuthenticateUser(UserLoginDto userLoginDto);
        Task<string> CreateToken();
    }
}
