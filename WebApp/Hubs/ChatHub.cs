using System.Text.Json;
using Message.Abstraction;
using Message.Dto;
using Microsoft.AspNetCore.SignalR;

namespace WebApp.Hubs;

public class Message
{
    public  Guid From  { get; set; }
    public  Guid To { get; set; }
    public string? Content  { get; set; }
}

public class ChatHub : Hub
{
    private readonly IMessageService _messageService;

    public ChatHub(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public async Task SendMessage(string dataString)
    {
        CreatingMessageDto? message = 
            JsonSerializer.Deserialize<CreatingMessageDto>(dataString);

        if (message != null)
        {
            var result = await _messageService.CreateAsync(message);
            Console.WriteLine("Message1");
            
            await Clients.All.SendAsync("ReceiveMessage", dataString);
        }
    }
    
    public override Task OnConnectedAsync()
    {
        Console.WriteLine($"{Context.ConnectionId} connected");
        
        return base.OnConnectedAsync();
    }
    
    public override async Task OnDisconnectedAsync(Exception e)
    {
        Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
        await base.OnDisconnectedAsync(e);
    }
}