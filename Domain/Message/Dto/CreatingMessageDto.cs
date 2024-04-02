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
        get
        {
            string[] data = new string[2] ;
            
            data[0] = From.ToString();
            data[1] = To.ToString();
	
            Array.Sort(data);
		
            return String.Join("-", data);
        }
    }
}