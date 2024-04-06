namespace MessageBroker;

public class MessageBroker : IMessageBroker
{
    public void SendMessage(string queueName, string message)
    {
        throw new NotImplementedException();
    }

    public void ConsumeMessage(string queueName, string message)
    {
        throw new NotImplementedException();
    }
}