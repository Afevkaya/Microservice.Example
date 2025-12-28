using Shared;
using Shared.Events;
using Stock.Repositories;
using Stock.Services.Messaging;

namespace Stock.Services;
    
public class StockService(IStockRepository stockRepository,IMessageSender messageSender, IMessagePublisher messagePublisher) : IStockService
{
    public async Task InitializeStockAsync()
    {
        var isCollection = await stockRepository.FindAsync();
        if (!isCollection)
        {
            var stocks = new List<Entities.Stock>
            {
                new()
                {
                    Id = Guid.NewGuid(),
                    ProductId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Count = 100
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    ProductId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Count = 200
                },
                new()
                {
                    Id = Guid.NewGuid(),
                    ProductId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Count = 300
                }
            };

            foreach (var stock in stocks)
            {
                await stockRepository.CreateStockAsync(stock);
            }
        }
    }

    public async Task HandleStockReservedAsync(OrderCreatedEvent orderCreatedEvent)
    {
        var stockResults = new List<bool>();
        var stocksToUpdate = new List<Entities.Stock>();

        foreach (var orderItem in orderCreatedEvent.OrderItems)
        {
            var stock = await stockRepository.GetStockAsync(orderItem.ProductId);
            if (stock != null && stock.Count >= orderItem.Count)
            {
                stockResults.Add(true);
                stock.Count -= orderItem.Count;
                stocksToUpdate.Add(stock);
            }
            else
            {
                stockResults.Add(false);
            }
        }

        if (stockResults.TrueForAll(result => result))
        {
            foreach (var stock in stocksToUpdate)
            {
                await stockRepository.UpdateStockAsync(stock.ProductId, stock);
            }

            StockReservedEvent stockReservedEvent = new()
            {
                BuyerId = orderCreatedEvent.BuyerId,
                OrderId = orderCreatedEvent.OrderId,
                TotalPrice = orderCreatedEvent.TotalPrice
            };

            await messageSender.SendAsync(RabbitMqSettings.Stock_OrderCreatedEventQueue, stockReservedEvent);
        }
        else
        {
            StockNotReservedEvent stockNotReservedEvent = new()
            {
                BuyerId = orderCreatedEvent.BuyerId,
                OrderId = orderCreatedEvent.OrderId,
                Message = "Not enough stock"
            };
            
            await messagePublisher.PublishAsync(stockNotReservedEvent);
        }
    }
}