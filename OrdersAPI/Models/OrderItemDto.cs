using OrdersAPI.Entities;

namespace OrdersAPI.Models
{
    public class OrderItemDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}