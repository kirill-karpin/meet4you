using Entities.Abstractions;

namespace Message;

public class Message : BaseEntity
{
    public int From { set; get; }
    public int To { set; get; }
    public DateTime Date { set; get; }
    public bool IsRead { set; get; }
    public string Content { set; get; }
}