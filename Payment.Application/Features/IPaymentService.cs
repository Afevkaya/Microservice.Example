using Shared.Events;

namespace Payment.Application.Features;

public interface IPaymentService
{
    Task HandleStockReservedAsync(StockReservedEvent stockReservedEvent);
}