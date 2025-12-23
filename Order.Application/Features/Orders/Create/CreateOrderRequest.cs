using Order.Application.Features.OrderItems.Create;

namespace Order.Application.Features.Orders.Create;

public record CreateOrderRequest(Guid BuyerId, List<CreateOrderItemRequest> OrderItems);