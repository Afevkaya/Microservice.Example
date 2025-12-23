using Order.Application.Contracts.Infrastructure;

namespace Order.Infrastructure.Orders;

public class OrderRepository(OrderServiceDbContext dbContext): IOrderRepository
{
    public async Task CreateOrderAsync(Domain.Entites.Order order)
    {
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync();
    }
}