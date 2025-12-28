using MassTransit;
using Order.Application.Features.Orders;
using Shared.Events;

namespace Order.Infrastructure.Messaging.Consumers;

public class PaymentFailedEventConsumer(IOrderService orderService): IConsumer<PaymentFailedEvent>
{
    public async Task Consume(ConsumeContext<PaymentFailedEvent> context)
    {
        await orderService.HandlePaymentFailedAsync(context.Message);
    }
}