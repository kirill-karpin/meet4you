namespace MessageBroker;

public class QueueMessage
{
    public const string DefaultQueue = "default-queue";
    public string QueueName { get; set; } = DefaultQueue;
    
    public required string Message { get; set;}
    
    public object Data { get;set; }
    
}