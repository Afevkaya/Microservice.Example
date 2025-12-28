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
        var order = new Domain.Entities.Order
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
            TotalPrice = order.TotalPrice
        };
        await publisher.PublishAsync(orderCreatedEvent);
        
    }
    public async Task HandlePaymentCompletedAsync(PaymentCompletedEvent paymentCompletedEvent)
    {
        var order = await orderRepository.GetOrderAsync(paymentCompletedEvent.OrderId);
        if (order is null)
        {
            throw new Exception("Order not found");
        }

        order.Status = OrderStatus.Completed;
        await orderRepository.UpdateOrderAsync(order);
    }

    public async Task HandleStockNotReservedAsync(StockNotReservedEvent stockNotReservedEvent)
    {
        var order = await orderRepository.GetOrderAsync(stockNotReservedEvent.OrderId);
        if (order is null)
        {
            throw new Exception("Order not found");
        }

        order.Status = OrderStatus.Failed;
        await orderRepository.UpdateOrderAsync(order);
    }

    public async Task HandlePaymentFailedAsync(PaymentFailedEvent paymentFailedEvent)
    {
        var order = await orderRepository.GetOrderAsync(paymentFailedEvent.OrderId);
        if (order is null)
        {
            throw new Exception("Order not found");
        }

        order.Status = OrderStatus.Failed;
        await orderRepository.UpdateOrderAsync(order);
    }

    
}