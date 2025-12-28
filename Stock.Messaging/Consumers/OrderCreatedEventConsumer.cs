using MassTransit;
using Shared.Events;
using Stock.Services;

namespace Stock.Messaging.Consumers;

public class OrderCreatedEventConsumer(IStockService stockService): IConsumer<OrderCreatedEvent>
{
    public Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        stockService.HandleStockReservedAsync(context.Message);
        return Task.CompletedTask;
    }
}