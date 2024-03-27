using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

public class NotificationHub : Hub
{
    public delegate Task NotificationDelegate(string user, string message);

    public static NotificationDelegate Notify;

    public override async Task OnConnectedAsync()
    {
        Notify += NotifyClient;
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Notify -= NotifyClient;
        await base.OnDisconnectedAsync(exception);
    }

    private Task NotifyClient(string user, string message)
    {
        return Clients.All.SendAsync("ReceiveMessage", user, message);
    }
    
}