using Payment.Application.Messaging;
using Shared.Events;

namespace Payment.Application.Features;

public class PaymentService(IMessagePublisher publisher): IPaymentService
{
    public async Task ProcessPaymentAsync(StockReservedEvent stockReservedEvent)
    {
        if (true)
        {
            
            await publisher.PublishAsync(new PaymentCompletedEvent
            {
                OrderId = stockReservedEvent.OrderId
            });
        }
    }
}