using Common;
using Common.Models;
using Microsoft.AspNetCore.SignalR;
using WebApp.Hubs;

namespace WebApp.Service;

public class EventBusService : IEventBusService
{
    private readonly IHubContext<MainHub> _eventBusHub;
    private readonly ConnectionPool _connectionPool;


    public EventBusService(IHubContext<MainHub> eventBusHub, ConnectionPool connectionPool)
    {
        _eventBusHub = eventBusHub;
        _connectionPool = connectionPool;
    }

    public async Task ReceiveNotification(string receiver, NotificationModel model)
    {
        var eventMessage = EventMessage.GetPersonalNotificationFabricMethod(
            receiver,
            model);
        await ReceiveEvent(eventMessage);
    }

    public async Task ReceiveEvent(EventMessage eventMessage)
    {
        if (eventMessage.isBroadcast())
        {
            await _eventBusHub.Clients.All.SendAsync("ReceiveEvent", eventMessage);
        }

        if (eventMessage.isPersonal())
        {
            foreach (var userId in eventMessage.Receivers)
            {
                var clientsList = _connectionPool.GetConnectionsByUserId(userId);
                await _eventBusHub.Clients.Clients(clientsList).SendAsync("ReceiveEvent", eventMessage);
            }
        }
    }

    public async Task NotifyAll()
    {
        var eventMessage = EventMessage.GetBroadcastMessageFabricMethod(
            EventSubscriber.Status,
            new StatusModel()
            {
                Count = _connectionPool.GetCount()
            });
        await ReceiveEvent(eventMessage);
    }
}