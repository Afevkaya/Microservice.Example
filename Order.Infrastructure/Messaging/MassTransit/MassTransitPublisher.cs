using MassTransit;
using Order.Application.Contracts.Messaging;
using Shared.Events;

namespace Order.Infrastructure.Messaging.MassTransit;

public class MassTransitPublisher(IPublishEndpoint publishEndpoint): IMessagePublisher
{
    public Task PublishAsync<T>(T message) where T : class
    {
        return publishEndpoint.Publish<T>(message);
    }
}