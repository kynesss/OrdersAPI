using Microsoft.AspNetCore.Mvc;
using OrdersAPI.Models;
using OrdersAPI.Services;

namespace OrdersAPI.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            await _accountService.Register(dto);
            return Ok();
        }
    }
}