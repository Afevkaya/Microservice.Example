using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Order.Application.Contracts.Messaging;
using Order.Infrastructure.Messaging.Consumers;
using Order.Infrastructure.Messaging.MassTransit;
using Shared;

namespace Order.Infrastructure.Extensions;

public static class MessageExtension
{
    public static IServiceCollection AddMessageBus(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumer<PaymentCompletedEventConsumer>();
            config.AddConsumer<StockNotReservedEventConsumer>();
            config.AddConsumer<PaymentFailedEventConsumer>();
            config.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMq"]);
                cfg.ReceiveEndpoint(RabbitMqSettings.Order_PaymentCompletedEventQueue,e => e.ConfigureConsumer<PaymentCompletedEventConsumer>(context));
                cfg.ReceiveEndpoint(RabbitMqSettings.Order_StockNotReservedEventQueue,e => e.ConfigureConsumer<StockNotReservedEventConsumer>(context));
                cfg.ReceiveEndpoint(RabbitMqSettings.Order_PaymentFailedEventQueue,e => e.ConfigureConsumer<PaymentFailedEventConsumer>(context));
            });
        });
        services.AddScoped<IMessagePublisher, MassTransitPublisher>();   
        return services;
    }
}