namespace Message.Dto;

public class MessageDto
{
    private int From { set; get; }
    private int To { set; get; }
    private DateTime Date { set; get; }
    private bool IsRead { set; get; }
    private string Content { set; get; }
}