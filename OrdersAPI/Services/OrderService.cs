using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Constants;
using OrdersAPI.Entities;
using OrdersAPI.Exceptions;
using OrdersAPI.Models;

namespace OrdersAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly IUserContextService _userContext;
        private readonly IMapper _mapper;

        public OrderService(AppDbContext dbContext, UserManager<User> userManager, IMapper mapper, IUserContextService userContext)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
            _userContext = userContext;
        }

        public async Task<int> Create(CreateOrderDto dto)
        {
            var orderItems = new List<OrderItem>();

            for (int i = 0; i < dto.Items.Count; i++)
            {
                var itemDto = dto.Items[i];
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == itemDto.ProductId);

                if (product is null)
                {
                    throw new NotFoundException($"Product with id {itemDto.ProductId} does not exist");
                }

                var orderItem = _mapper.Map<OrderItem>(itemDto);
                orderItem.UnitPrice = product.Price;

                orderItems.Add(orderItem);
            }

            var order = new Order
            {
                CreatedAt = DateTime.Now,
                OrderItems = orderItems,
                UserId = _userContext.UserId
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            return order.Id;
        }

        public async Task Delete(int id)
        {
            var order = await _dbContext.Orders
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order is null)
            {
                throw new NotFoundException($"Order with id {id} does not exist");
            }

            var isCreatorOrAdmin = _userContext.UserId == order.UserId || _userContext.User!.IsInRole(Roles.Admin);

            if (!isCreatorOrAdmin)
            {
                throw new ForbidException("Only order creator or admin is allowed to delete");
            }
        }

        public async Task<IEnumerable<OrderDto>> GetAll()
        {
            var orders = await _dbContext.Orders
                .Include(order => order.User)
                .Include(order => order.OrderItems)
                .ThenInclude(orderItem => orderItem.Product)
                .ToListAsync();

            var dtos = new List<OrderDto>();

            foreach (var order in orders)
            {
                var orderItemsDto = _mapper.Map<List<OrderItemDto>>(order.OrderItems);

                var orderDto = _mapper.Map<OrderDto>(order);
                var creator = order.User;

                orderDto.CreatedBy = $"{creator.FirstName} {creator.LastName}";
                dtos.Add(orderDto);
            }

            return dtos;
        }

        public async Task<OrderDto> GetById(int id)
        {
            var order = await _dbContext.Orders
                .Include(order => order.OrderItems)
                .ThenInclude(orderItem => orderItem.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (order is null)
            {
                throw new NotFoundException($"Order with id {id} does not exist");
            }

            var creator = order.User;
            var dto = _mapper.Map<OrderDto>(order);
            dto.CreatedBy = $"{creator.FirstName} {creator.LastName}";

            return dto;
        }
    }
}