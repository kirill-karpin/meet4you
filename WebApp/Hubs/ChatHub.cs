using System.Text.Json;
using Common;
using Common.Models;
using Message.Abstraction;
using Message.Dto;
using MessageBroker;
using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs;

public class ChatHub : Hub
{
    private readonly IMessageService _messageService;
    private readonly IMessageBroker _messageBroker;

    private static readonly ConnectionPool ConnectionPool = new();

    public ChatHub(IMessageService messageService, IMessageBroker messageBroker)
    {
        _messageService = messageService;
        _messageBroker = messageBroker;
    }

    public string? GetToken()
    {
        var http = Context?.GetHttpContext();
        return http?.Request.Query["token"].ToString();
    }

    public string? GetConnectionId()
    {
        return Context.ConnectionId;
    }

    public async Task SendMessage(string dataString)
    {
        CreatingMessageDto? message =
            JsonSerializer.Deserialize<CreatingMessageDto>(dataString);

        Console.WriteLine($"{Context?.ConnectionId} SendMessage");

        if (message != null)
        {
            Console.WriteLine($"{Context.ConnectionId} try save DB");

            await _messageService.CreateAsync(message);

            Console.WriteLine($"{Context.ConnectionId} try get connections");
            var clientsList = ConnectionPool.GetConnectionsByUserId(message.To.ToString());

            await Clients.Clients(clientsList).SendAsync("ReceiveMessage", dataString);

            _messageBroker.SendMessage(new QueueMessage
            {
                Message = JsonSerializer.Serialize(EventMessage.GetListNotificationFabricMethod(
                    clientsList,
                    new NotificationModel()
                    {
                        Content = "Новое сообщение",
                        Title = "Новое сообщение"
                    }))
            });

            var senderList = ConnectionPool.GetConnectionsByUserId(message.From.ToString());

            Console.WriteLine($"{Context.ConnectionId} try receive to caller");

            await Clients.Clients(senderList).SendAsync("ReceiveMessage", dataString);
            ;
        }
    }

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} connected");

        var token = GetToken();
        var id = GetConnectionId();

        if (token is not null && id is not null)
        {
            ConnectionPool.Update(new UpdatePoolAction
            {
                UserId = token,
                ConnectionId = id
            });
            return base.OnConnectedAsync();
        }

        throw new Exception("No user token");
    }

    public override async Task OnDisconnectedAsync(Exception e)
    {
        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
        var token = GetToken();
        var id = GetConnectionId();

        if (token is not null && id is not null)
        {
            ConnectionPool.Update(new UpdatePoolAction
            {
                UserId = token,
                ConnectionId = id,
                IsDelete = true
            });
        }

        await base.OnDisconnectedAsync(e);
    }
}