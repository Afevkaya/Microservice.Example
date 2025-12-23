using Order.Domain.Entites;

namespace Order.Application.Contracts.Infrastructure;

public interface IOrderRepository
{
    Task CreateOrderAsync(Domain.Entites.Order order);
}