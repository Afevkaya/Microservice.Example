using Order.Application.Contracts.Infrastructure;
using Order.Application.Contracts.Messaging;
using Order.Application.Features.Orders.Create;
using Order.Domain.Enums;
using Shared.Events;
using Shared.Messages;

namespace Order.Application.Features.Orders;

public class OrderService(IOrderRepository orderRepository, IMessagePublisher publisher): IOrderService
{
    public async Task CreateOrderAsync(CreateOrderRequest request)
    {
        var order = new Domain.Entites.Order
        {
            Id = Guid.NewGuid(),
            BuyerId = request.BuyerId,
            CreatedDate = DateTime.Now,
            Status = OrderStatus.Suspended,
            OrderItems = request.OrderItems.Select(oi => new Domain.Entites.OrderItem
            {
                ProductId = oi.ProductId,
                Count = oi.Count,
                Price = oi.Price
            }).ToList(),
            TotalPrice = request.OrderItems.Sum(oi => (oi.Price * oi.Count))
        };
        await orderRepository.CreateOrderAsync(order);
        
        var orderCreatedEvent = new OrderCreatedEvent
        {
            OrderId = order.Id,
            BuyerId = order.BuyerId,
            OrderItems = order.OrderItems.Select(oi => new OrderItemMessage
            {
                ProductId = oi.ProductId,
                Count = oi.Count,
            }).ToList(),
        };
        await publisher.PublishAsync(orderCreatedEvent);
        
    }
}