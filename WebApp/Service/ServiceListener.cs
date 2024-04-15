using System.Diagnostics;
using System.Text;
using System.Text.Json;
using Common;
using MessageBroker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using WebApp.Service;

namespace WebApp.Service;

public class ServiceListener : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private readonly IConfiguration _configuration;
    private readonly IEventBusService _eventBusService;


    public ServiceListener(IConfiguration configuration, IEventBusService eventBusService)
    {
        _configuration = configuration;
        _eventBusService = eventBusService;


        var rabbitMqConfig = _configuration.GetSection("RabbitMQ");
        var factory = new ConnectionFactory()
        {
            HostName = rabbitMqConfig["HostName"],
            Port = int.Parse(rabbitMqConfig["Port"] ?? "0"),
            UserName = rabbitMqConfig["UserName"],
            Password = rabbitMqConfig["Password"],
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: QueueMessage.DefaultQueue, durable: false, exclusive: false, autoDelete: false,
            arguments: null);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.Received += async (ch, ea) =>
        {
            var content = Encoding.UTF8.GetString(ea.Body.ToArray());

            Console.WriteLine($"Hande message: {content}");

            var queueMessage = JsonSerializer.Deserialize<QueueMessage>(content);
            if (queueMessage != null)
            {
                await RouteHandlerServices(queueMessage);
            }

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

    public Task RouteHandlerServices(QueueMessage queueMessage)
    {
        switch (queueMessage.Service)
        {
            case ServicesEnum.Notification:
                EventMessage? message = JsonSerializer.Deserialize<EventMessage>(queueMessage.Message);
                if (message != null) _eventBusService.ReceiveEvent(message);

                break;
            default:
                throw new Exception("Unknown service handler");
        }

        return Task.FromResult(true);
    }
}