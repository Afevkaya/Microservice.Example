using Microsoft.Extensions.DependencyInjection;

namespace Stock.Repositories.Extensions;

public static class MongoDbExtension
{
    public static IServiceCollection AddMongoDb(this IServiceCollection services)
    {
        services.AddSingleton<MongoDbServices>();
        return services;
    }
}