using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Contracts.Infrastructure;
using Order.Infrastructure.Orders;

namespace Order.Infrastructure.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<OrderServiceDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
        });
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }
}