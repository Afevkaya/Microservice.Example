using MassTransit;
using Payment.Application.Features;
using Shared.Events;

namespace Payment.Messaging.Consumers;

public class StockReservedEventConsumer(IPaymentService paymentService): IConsumer<StockReservedEvent>
{
    public async Task Consume(ConsumeContext<StockReservedEvent> context)
    {
        await paymentService.ProcessPaymentAsync(context.Message);
    }
}