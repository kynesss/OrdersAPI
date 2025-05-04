using OrdersAPI.Entities;

namespace OrdersAPI.Models
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}