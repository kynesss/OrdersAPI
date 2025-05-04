using System.Security.Claims;

namespace OrdersAPI.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal? User => _httpContextAccessor.HttpContext?.User;
        public string? UserId => User?.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}