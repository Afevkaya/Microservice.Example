namespace Order.Application.Features.OrderItems.Create;

public record CreateOrderItemRequest(Guid ProductId, int Count, decimal Price);