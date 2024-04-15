namespace MessageBroker;

public class QueueMessage
{
    public const string DefaultQueue = "default-queue";
    public string QueueName { get; set; } = DefaultQueue;
    public List<Guid>? Receivers { get; set;  }
    public ServicesEnum Service { get; set;  }
    public required string Message { get; set;}
    
    public object Data { get;set; }
}