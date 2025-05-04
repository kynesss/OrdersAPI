using OrdersAPI.Models;

namespace OrdersAPI.Services
{
    public interface IOrderService
    {
        Task<int> Create(CreateOrderDto dto);

        Task<IEnumerable<OrderDto>> GetAll();

        Task<OrderDto> GetById(int id);
    }
}