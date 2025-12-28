using MassTransit;
using Order.Application.Features.Orders;
using Shared.Events;

namespace Order.Infrastructure.Messaging.Consumers;

public class StockNotReservedEventConsumer(IOrderService orderService) : IConsumer<StockNotReservedEvent>
{
    public async Task Consume(ConsumeContext<StockNotReservedEvent> context)
    {
        await orderService.HandleStockNotReservedAsync(context.Message);
    }
}