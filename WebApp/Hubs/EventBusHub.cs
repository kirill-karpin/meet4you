using Common;
using Common.Models;
using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs;

public class EventBusHub : Hub
{
    private static readonly ConnectionPool ConnectionPool = new();


    public string? GetToken()
    {
        var http = Context?.GetHttpContext();
        return http?.Request.Query["token"].ToString();
    }

    public string? GetConnectionId()
    {
        return Context.ConnectionId;
    }

    public async Task ReceiveEvent(EventMessage eventMessage)
    {
        if (eventMessage.isBroadcast())
        {
            await Clients.All.SendAsync("ReceiveEvent", eventMessage);
        }

        if (eventMessage.isPersonal())
        {
            foreach (var userId in eventMessage.Receivers)
            {
                var clientsList = ConnectionPool.GetConnectionsByUserId(userId);
                await Clients.Clients(clientsList).SendAsync("ReceiveEvent", eventMessage);
            }
        }
    }

    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} connected");

        var userId = GetToken();
        var connectionId = GetConnectionId();

        if (!String.IsNullOrEmpty(userId)  && !String.IsNullOrEmpty( connectionId))
        {
            ConnectionPool.Update(new UpdatePoolAction
            {
                UserId = userId,
                ConnectionId = connectionId
            });
            NotifyAll();
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
            ConnectionPool.Update(new UpdatePoolAction
            {
                UserId = userId,
                ConnectionId = connectionId,
                IsDelete = true
            });
            NotifyAll();
        }

        await base.OnDisconnectedAsync(e);
    }

    public async Task NotifyAll()
    {
        var eventMessage = EventMessage.GetBroadcastMessageFabricMethod(
            EventSubscriber.Status, 
            new StatusModel()
            {
                Count = ConnectionPool.GetCount()
            });
        await ReceiveEvent(eventMessage);
    }
    
    public async Task FetchStatus()
    {
        var receiver = GetToken();
        if (!String.IsNullOrEmpty(receiver))
        {
            var eventMessage = EventMessage.GetPersonalMessageFabricMethod(
                EventSubscriber.Status, 
                receiver, 
                new StatusModel()
                {
                    Count = ConnectionPool.GetCount()
                });
            await ReceiveEvent(eventMessage);
        }
    }
}