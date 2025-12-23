using Order.Application.Features.Orders.Create;

namespace Order.Application.Features.Orders;

public interface IOrderService
{
    Task CreateOrderAsync(CreateOrderRequest request);
}