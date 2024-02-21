using Entities.Abstractions;

namespace Message;

public class Message : BaseEntity
{

    private Guid _from;
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
}