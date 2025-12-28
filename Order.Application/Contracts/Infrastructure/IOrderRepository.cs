
namespace Order.Application.Contracts.Infrastructure;

public interface IOrderRepository
{
    Task CreateOrderAsync(Domain.Entities.Order order);
    Task<Domain.Entities.Order> GetOrderAsync(Guid id);
    Task UpdateOrderAsync(Domain.Entities.Order order);
}