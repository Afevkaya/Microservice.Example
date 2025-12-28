using Order.Application.Features.Orders.Create;
using Shared.Events;

namespace Order.Application.Features.Orders;

public interface IOrderService
{
    Task CreateOrderAsync(CreateOrderRequest request);
    Task HandlePaymentCompletedAsync(PaymentCompletedEvent paymentCompletedEvent);
    Task HandleStockNotReservedAsync(StockNotReservedEvent stockNotReservedEvent);
}