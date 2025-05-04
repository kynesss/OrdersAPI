using System.Security.Claims;

namespace OrdersAPI.Services
{
    public interface IUserContextService
    {
        ClaimsPrincipal? User { get; }
        string? UserId { get; }
    }
}