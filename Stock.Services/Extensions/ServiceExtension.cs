using Microsoft.Extensions.DependencyInjection;

namespace Stock.Services.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IStockService, StockService>();
        return services;
    }
}