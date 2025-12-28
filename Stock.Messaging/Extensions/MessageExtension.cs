using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shared;
using Stock.Messaging.Consumers;
using Stock.Messaging.Publishers;
using Stock.Services.Messaging;

namespace Stock.Messaging.Extensions;

public static class MessageExtension
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumer<OrderCreatedEventConsumer>();
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq"]);
                cfg.ReceiveEndpoint(RabbitMqSettings.Stock_OrderCreatedEventQueue, 
                    c => c.ConfigureConsumer<OrderCreatedEventConsumer>(context));
            });
        });

        services.AddScoped<IMessageSender, MassTransitMessageSender>();
        services.AddScoped<IMessagePublisher, MassTransitMessagePublisher>();
        return services;
    }
}