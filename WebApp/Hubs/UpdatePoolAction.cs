namespace WebApp.Hubs;

public class UpdatePoolAction
{
    public bool IsDelete { set; get; } = false;
    public required string UserId { set; get; }
    
    public required string ConnectionId { set; get; }
}