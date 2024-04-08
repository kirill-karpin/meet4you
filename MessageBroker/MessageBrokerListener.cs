using System.Diagnostics;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace MessageBroker;

public class MessageBrokerListener  : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private readonly IConfiguration _configuration;

    public MessageBrokerListener(IConfiguration configuration)
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
        
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: QueueMessage.DefaultQueue, durable: false, exclusive: false, autoDelete: false, arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());
			
            // Каким-то образом обрабатываем полученное сообщение
            Console.WriteLine($"Получено сообщение: {content}");

            _channel.BasicAck(ea.DeliveryTag, false);
        };

         _channel.BasicConsume(QueueMessage.DefaultQueue, false, consumer);

        return Task.CompletedTask;
    }
	
    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}