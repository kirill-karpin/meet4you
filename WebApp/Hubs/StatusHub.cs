using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs;



public static class UserHandler
{
    public static HashSet<string> ConnectedIds = new HashSet<string>();
}

//TODO ADD ONLINE STATUS SERVICE

public class StatusHub : Hub
{
    public async Task FetchStatus()
    {
        var count = UserHandler.ConnectedIds.Count();
        
        var status = new AppStatus() {
            OnlineCount = count
        };
        
        Console.WriteLine("FetchStatus");
        
        await Clients.All.SendAsync("ReceiveStatus", status);
    }
    
    public override Task OnConnectedAsync()
    {
        UserHandler.ConnectedIds.Add(Context.ConnectionId);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        UserHandler.ConnectedIds.Remove(Context.ConnectionId);

        return base.OnDisconnectedAsync(exception);
    }
}

public class AppStatus
{
    public int OnlineCount { set; get; } = 0;
}