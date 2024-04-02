using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Text;

namespace WebApp.Service;

public interface IRabbitMQService
{
    void SendMessage(string queueName, string message);
}

public class RabbitMQService : IRabbitMQService, IDisposable
{
    private readonly IConfiguration _configuration;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQService(IConfiguration configuration)
    {
        _configuration = configuration;

        /*var rabbitMQConfig = _configuration.GetSection("RabbitMQ");
        var factory = new ConnectionFactory()
        {
            HostName = rabbitMQConfig["HostName"],
            Port = int.Parse(rabbitMQConfig["Port"]),
            UserName = rabbitMQConfig["UserName"],
            Password = rabbitMQConfig["Password"],
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();*/
    }

    public void SendMessage(string queueName, string message)
    {
        _channel.QueueDeclare(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body: body);

        Console.WriteLine(" [x] Sent {0}", message);
    }

    public void Dispose()
    {
        _channel.Close();
        _connection.Close();
    }
}