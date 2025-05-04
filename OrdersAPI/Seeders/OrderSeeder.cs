using OrdersAPI.Entities;

namespace OrdersAPI.Seeders
{
    public class OrderSeeder : ISeeder
    {
        private readonly AppDbContext _dbContext;

        public OrderSeeder(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Seed()
        {
            if (await _dbContext.Database.CanConnectAsync())
            {
                if (!_dbContext.Products.Any())
                {
                    var products = GetProducts();
                    await _dbContext.Products.AddRangeAsync(products);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.Orders.Any())
                {
                    var user = _dbContext.Users.FirstOrDefault(u => u.Email == "user@orders.com");

                    var orders = new List<Order>()
                    {
                        new() {
                            CreatedAt = DateTime.UtcNow.AddDays(-1),
                            UserId = user.Id
                        }
                    };

                    await _dbContext.Orders.AddRangeAsync(orders);
                    await _dbContext.SaveChangesAsync();
                }

                if (!_dbContext.OrderItems.Any())
                {
                    var orderItems = GetOrderItems();
                    await _dbContext.OrderItems.AddRangeAsync(orderItems);
                    await _dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<OrderItem> GetOrderItems()
        {
            var firstOrderId = _dbContext.Orders.FirstOrDefault()!.Id;

            var orderItems = new List<OrderItem>
                    {
                        new() {
                            OrderId = firstOrderId,
                            ProductId = 1, // Pizza Margherita
                            Quantity = 2,
                            UnitPrice = 25.99m
                        },
                        new() {
                            OrderId = firstOrderId,
                            ProductId = 6, // Coca-Cola
                            Quantity = 1,
                            UnitPrice = 5.00m
                        }
                    };

            return orderItems;
        }

        private IEnumerable<Product> GetProducts()
        {
            return [
                    new Product { Name = "Pizza Margherita", Description = "Klasyczna pizza z serem mozzarella", Price = 25.99m },
                    new Product { Name = "Pizza Pepperoni", Description = "Pizza z pikantną kiełbasą pepperoni", Price = 28.99m },
                    new Product { Name = "Spaghetti Bolognese", Description = "Makaron z mięsnym sosem pomidorowym", Price = 22.50m },
                    new Product { Name = "Burger Classic", Description = "Wołowy burger z warzywami", Price = 19.90m },
                    new Product { Name = "Frytki", Description = "Złociste frytki z solą", Price = 7.50m },
                    new Product { Name = "Coca-Cola 0.5L", Description = "Zimny napój gazowany", Price = 5.00m },
                    new Product { Name = "Woda mineralna 0.5L", Description = "Woda niegazowana", Price = 3.50m },
                    new Product { Name = "Tiramisu", Description = "Deser na bazie mascarpone i kawy", Price = 12.00m }];
        }
    }
}