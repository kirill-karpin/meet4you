using Common;

namespace MessageBroker;

public interface IMessageBroker
{
    public void SendMessage(QueueMessage queueMessage);
    
}