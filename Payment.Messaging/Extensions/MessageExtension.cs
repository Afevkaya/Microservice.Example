using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payment.Application.Messaging;
using Payment.Messaging.Consumers;
using Payment.Messaging.Publishers;
using Shared;

namespace Payment.Messaging.Extensions;

public static class MessageExtension
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumer<StockReservedEventConsumer>();
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq"]);
                cfg.ReceiveEndpoint(RabbitMqSettings.Payment_StockReservedEventQueue, 
                    c => c.ConfigureConsumer<StockReservedEventConsumer>(context));
            });
        });

        services.AddScoped<IMessagePublisher, MassTransitPublisher>();
        
        return services;
    }
}