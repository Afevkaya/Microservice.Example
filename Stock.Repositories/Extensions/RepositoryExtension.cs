using Microsoft.Extensions.DependencyInjection;

namespace Stock.Repositories.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IStockRepository, StockRepository>();
        
        return services;
    }
}