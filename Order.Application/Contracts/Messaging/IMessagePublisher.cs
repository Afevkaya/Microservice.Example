namespace Order.Application.Contracts.Messaging;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message) where T : class;
}