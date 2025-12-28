namespace Stock.Services.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message) where T : class;
}