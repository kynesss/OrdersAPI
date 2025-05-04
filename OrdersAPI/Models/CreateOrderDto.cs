using OrdersAPI.Entities;

namespace OrdersAPI.Models
{
    public class CreateOrderDto
    {
        public List<CreateOrderItemDto> Items { get; set; }
    }
}