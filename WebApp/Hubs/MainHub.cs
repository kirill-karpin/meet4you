using Common;
using Common.Models;
using MessageBroker;
using Microsoft.AspNetCore.SignalR;
using WebApp.Service;


namespace WebApp.Hubs;

public class MainHub : Hub
{
    private readonly ConnectionPool _connectionPool;
    private readonly IMessageBroker _messageBroker;

    public MainHub(ConnectionPool connectionPool, IMessageBroker messageBroker)
    {
        _connectionPool = connectionPool;
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


    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} connected");

        var userId = GetToken();
        var connectionId = GetConnectionId();

        if (!String.IsNullOrEmpty(userId) && !String.IsNullOrEmpty(connectionId))
        {
            _connectionPool.Update(new UpdatePoolAction
            {
                UserId = userId,
                ConnectionId = connectionId
            });
            SendNotifyAllMessage();
            return base.OnConnectedAsync();
        }

        throw new Exception("Unknown token");
    }

    public override async Task OnDisconnectedAsync(Exception e)
    {
        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");

        var userId = GetToken();
        var connectionId = GetConnectionId();

        if (!String.IsNullOrEmpty(userId) && !String.IsNullOrEmpty(connectionId))
        {
            _connectionPool.Update(new UpdatePoolAction
            {
                UserId = userId,
                ConnectionId = connectionId,
                IsDelete = true
            });
            SendNotifyAllMessage();
        }

        await base.OnDisconnectedAsync(e);
    }
    
    public void SendNotifyAllMessage()
    {
        _messageBroker.SendMessage(new QueueMessage()
        {
            Message = "notify-all"
        });
    }
}