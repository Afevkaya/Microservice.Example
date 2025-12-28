using Shared.Events;

namespace Stock.Services;

public interface IStockService
{
    Task InitializeStockAsync();
    Task HandleStockReservedAsync(OrderCreatedEvent orderCreatedEvent);
}