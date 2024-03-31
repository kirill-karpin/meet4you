using Entities.Abstractions;

namespace Message;

public class Message : BaseEntity
{

    private Guid _from;
    private string _chatId;

    public Guid From
    {
        set
        {
            _from = value;
            CreatedBy = value;
            UpdatedBy = value;
        }
        get => _from;
    }

    public Guid To { set; get; }
    public bool IsRead { set; get; }
    public string Content { set; get; }

    public string ChatId
    {
        set => _chatId = value;
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