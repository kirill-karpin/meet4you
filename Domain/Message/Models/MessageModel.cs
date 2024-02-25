namespace Message.Models;

public class MessageModel
{
    private Guid From { set; get; }
    private Guid To { set; get; }
    private bool IsRead { set; get; }
    public string Content { set; get; }
}