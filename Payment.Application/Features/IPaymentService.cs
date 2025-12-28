using Shared.Events;

namespace Payment.Application.Features;

public interface IPaymentService
{
    Task ProcessPaymentAsync(StockReservedEvent stockReservedEvent);
}