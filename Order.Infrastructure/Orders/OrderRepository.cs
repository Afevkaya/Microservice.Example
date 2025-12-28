using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts.Infrastructure;

namespace Order.Infrastructure.Orders;

public class OrderRepository(OrderServiceDbContext dbContext): IOrderRepository
{
    public async Task CreateOrderAsync(Domain.Entities.Order order)
    {
        dbContext.Orders.Add(order);
        await dbContext.SaveChangesAsync();
    }

    public async Task<Domain.Entities.Order?> GetOrderAsync(Guid id)
    {
        return await dbContext.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    public Task UpdateOrderAsync(Domain.Entities.Order order)
    {
        dbContext.Orders.Update(order);
        return dbContext.SaveChangesAsync();
    }
}