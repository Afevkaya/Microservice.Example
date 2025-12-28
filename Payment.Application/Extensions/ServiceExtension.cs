using Microsoft.Extensions.DependencyInjection;
using Payment.Application.Features;

namespace Payment.Application.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPaymentService, PaymentService>();
        return services;
    }
}