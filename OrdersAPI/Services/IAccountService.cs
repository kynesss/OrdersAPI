using OrdersAPI.Models;

namespace OrdersAPI.Services
{
    public interface IAccountService
    {
        Task Register(RegisterUserDto dto);
    }
}