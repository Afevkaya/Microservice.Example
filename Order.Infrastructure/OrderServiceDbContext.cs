using Microsoft.EntityFrameworkCore;
using Order.Domain.Entites;

namespace Order.Infrastructure;

public class OrderServiceDbContext:DbContext
{
    public OrderServiceDbContext(DbContextOptions<OrderServiceDbContext> options) : base(options)
    {
        
    }
    public DbSet<Domain.Entites.Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
}