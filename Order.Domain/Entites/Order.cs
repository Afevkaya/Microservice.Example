using Order.Domain.Enums;

namespace Order.Domain.Entites;

public class Order
{
    public Guid Id { get; set; }
    public Guid BuyerId { get; set; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }
    public DateTime CreatedDate { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}