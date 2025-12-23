using Microsoft.Extensions.DependencyInjection;
using Order.Application.Features.Orders;

namespace Order.Application.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IOrderService, OrderService>();
        return services;
    }
}