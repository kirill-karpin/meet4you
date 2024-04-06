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

        var token = GetToken();
        var id = GetConnectionId();

        if (!String.IsNullOrEmpty(token)  && !String.IsNullOrEmpty( id))
        {
            ConnectionPool.Add(token, id);
            NotifyAll();
            return base.OnConnectedAsync();
        }

        return base.OnDisconnectedAsync(new Exception("No  token id"));
    }

    public override async Task OnDisconnectedAsync(Exception e)
    {
        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");

        var token = GetToken();
        var id = GetConnectionId();

        if (!String.IsNullOrEmpty(token)  && !String.IsNullOrEmpty( id))
        {
            ConnectionPool.Remove(token);
            NotifyAll();
        }

        await base.OnDisconnectedAsync(e);
    }

    public async Task NotifyAll()
    {
        var eventMessage = EventMessage.GetBroadcastMessageFabric(
            EventSubscriber.Status, 
            new StatusModel()
            {
                Count = ConnectionPool.GetCount()
            });
        await ReceiveEvent(eventMessage);
    }
    
    public async Task FetchStatus()
    {
        var token = GetToken();
        if (!String.IsNullOrEmpty(token))
        {
            var eventMessage = EventMessage.GetPersonalMessageFabric(
                EventSubscriber.Status, 
                token, 
                new StatusModel()
                {
                    Count = ConnectionPool.GetCount()
                });
            await ReceiveEvent(eventMessage);
        }
    }
}