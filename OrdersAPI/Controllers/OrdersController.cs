using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OrdersAPI.Controllers
{
    [Route("api/orders")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateOrderDto dto)
        //{
        //}
    }
}