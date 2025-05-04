using OrdersAPI.Models;

namespace OrdersAPI.Services
{
    public interface IAccountService
    {
        Task Register(RegisterDto dto);

        Task<string> GenerateJWT(LoginDto loginDto);
    }
}