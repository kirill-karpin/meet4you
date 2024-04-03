namespace Message.Dto;

public class CreatingMessageDto
{
    public Guid From { set; get; }
    public Guid To { set; get; }
    public bool IsRead { set; get; }
    public string Content { set; get; }
    
    private string _chatId;
    public string ChatId
    {
        get { return GetChatId(From.ToString(), To.ToString()); }
    }

    public static string GetChatId(string guid1, string guid2)
    {
        var data = new string[2];

        data[0] = guid1;
        data[1] = guid2;

        Array.Sort(data);

        return String.Join("-", data);
    }
}