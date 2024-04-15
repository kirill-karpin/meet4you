using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBroker;

public class MessageBrokerService : IMessageBroker
{
    private readonly IConfiguration _configuration;
    public IConnection Connection { get; }
    public IModel Channel  { get; }
    
    public MessageBrokerService(IConfiguration configuration)
    {
        _configuration = configuration;

        var rabbitMqConfig = _configuration.GetSection("RabbitMQ");
        var factory = new ConnectionFactory()
        {
            HostName = rabbitMqConfig["HostName"],
            Port = int.Parse(rabbitMqConfig["Port"]),
            UserName = rabbitMqConfig["UserName"],
            Password = rabbitMqConfig["Password"],
            DispatchConsumersAsync = true
        };

        Connection = factory.CreateConnection();
        Channel = Connection.CreateModel();
    }

    public void SendMessage(QueueMessage queueMessage)
    {
        Channel.QueueDeclare(queue: queueMessage.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
        
        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(queueMessage));

        Channel.BasicPublish(exchange: "",
            routingKey: queueMessage.QueueName,
            basicProperties: null,
            body: body);

        Console.WriteLine($"Send to message broker: {body}");
    }
}