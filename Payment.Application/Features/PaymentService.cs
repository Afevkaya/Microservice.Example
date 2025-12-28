using Payment.Application.Messaging;
using Shared.Events;

namespace Payment.Application.Features;

public class PaymentService(IMessagePublisher publisher): IPaymentService
{
    public async Task HandleStockReservedAsync(StockReservedEvent stockReservedEvent)
    {
        if (true)
        {
            
            await publisher.PublishAsync(new PaymentCompletedEvent
            {
                OrderId = stockReservedEvent.OrderId
            });
            Console.WriteLine("Payment completed");
        }
        else
        {
            await publisher.PublishAsync(new PaymentFailedEvent
            {
                OrderId = stockReservedEvent.OrderId,
                Message = "Payment failed due to insufficient funds."
            });
        }
    }
}