using Common;
using Common.Models;
using Microsoft.AspNetCore.SignalR;
using WebApp.Service;


namespace WebApp.Hubs;

public class EventBusHub : Hub
{
    private readonly ConnectionPool _connectionPool;
    private readonly IEventBusService _eventBusService;

    public delegate void EventHandler(object sender, EventArgs e);

    public static event EventHandler? MyStaticEvent;
    
    public EventBusHub(ConnectionPool connectionPool, IEventBusService eventBusService)
    {
        _connectionPool = connectionPool;
        _eventBusService = eventBusService;
    }
    
    public static void RaiseMyStaticEvent()
    {
        MyStaticEvent?.Invoke(null, EventArgs.Empty);
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

        if (!String.IsNullOrEmpty(userId)  && !String.IsNullOrEmpty( connectionId))
        {
            _connectionPool.Update(new UpdatePoolAction
            {
                UserId = userId,
                ConnectionId = connectionId
            });
            RaiseMyStaticEvent();
            return  base.OnConnectedAsync();
        }

        throw new Exception("Unknown token");
    }

    public override async Task OnDisconnectedAsync(Exception e)
    {
        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");

        var userId = GetToken();
        var connectionId = GetConnectionId();

        if (!String.IsNullOrEmpty(userId)  && !String.IsNullOrEmpty( connectionId))
        {
            _connectionPool.Update(new UpdatePoolAction
            {
                UserId = userId,
                ConnectionId = connectionId,
                IsDelete = true
            });
            RaiseMyStaticEvent();
        }

        await base.OnDisconnectedAsync(e);
    }
}
