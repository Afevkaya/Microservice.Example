using MassTransit;
using Stock.Services.Messaging;

namespace Stock.Messaging.Publishers;

public class MassTransitMessagePublisher(IPublishEndpoint publishEndpoint): IMessagePublisher
{
    public async Task PublishAsync<T>(T message) where T : class
    {
        await publishEndpoint.Publish<T>(message);
    }
}