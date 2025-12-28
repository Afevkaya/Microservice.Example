using MassTransit;
using Stock.Services.Messaging;

namespace Stock.Messaging.Publishers;

public class MassTransitMessageSender(ISendEndpointProvider sendEndpointProvider): IMessageSender
{
    public async Task SendAsync<T>(string queueName, T message) where T : class
    {
        var sendEndpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));
        await sendEndpoint.Send(message);
    }
}