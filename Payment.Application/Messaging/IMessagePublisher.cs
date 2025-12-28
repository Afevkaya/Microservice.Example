namespace Payment.Application.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message) where T : class;
}