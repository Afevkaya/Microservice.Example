using MassTransit;
using Shared.Events;
using Stock.Services;

namespace Stock.Messaging.Consumers;

public class OrderCreatedEventConsumer(IStockService stockService): IConsumer<OrderCreatedEvent>
{
    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        await stockService.HandleStockReservedAsync(context.Message);
    }
}