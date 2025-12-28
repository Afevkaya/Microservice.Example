using MassTransit;
using Order.Application.Features.Orders;
using Shared.Events;

namespace Order.Infrastructure.Messaging.Consumers;

public class PaymentCompletedEventConsumer(IOrderService orderService): IConsumer<PaymentCompletedEvent>
{
    public async Task Consume(ConsumeContext<PaymentCompletedEvent> context)
    {
        await orderService.ProcessPaymentAsync(context.Message);
    }
}