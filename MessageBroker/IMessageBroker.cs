namespace MessageBroker;

public interface IMessageBroker
{
    public void SendMessage(string queueName, string message);
    
    public void ConsumeMessage(string queueName, string message);
}