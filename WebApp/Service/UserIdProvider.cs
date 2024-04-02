using Microsoft.AspNetCore.SignalR;

namespace WebApp.Service;

public class UserIdProvider : IUserIdProvider
{
    public string GetUserId(HubConnectionContext connection)
    {
        string result =  connection.User?.Identity?.Name ?? "";

        return result;
    }
}