using Common;
using Common.Models;

namespace WebApp.Service;

public interface IEventBusService
{
    public Task ReceiveNotification(string receiver, NotificationModel model);
    public Task ReceiveEvent(EventMessage eventMessage);
    public Task NotifyAll();
}