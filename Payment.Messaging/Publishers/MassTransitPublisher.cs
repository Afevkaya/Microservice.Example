using MassTransit;
using Payment.Application.Messaging;

namespace Payment.Messaging.Publishers;

public class MassTransitPublisher(IPublishEndpoint publishEndpoint): IMessagePublisher
{
    public Task PublishAsync<T>(T message) where T : class
    {
        return publishEndpoint.Publish<T>(message);
    }
}