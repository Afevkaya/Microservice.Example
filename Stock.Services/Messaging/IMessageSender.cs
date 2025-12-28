namespace Stock.Services.Messaging;

public interface IMessageSender
{
    Task SendAsync<T>(string queueName,T message) where T : class;
}