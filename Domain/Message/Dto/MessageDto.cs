namespace Message.Dto;

public class MessageDto
{
    public Guid Id { get; set; }
    public int Sort { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool Deleted { get; set; }

    private int From { set; get; }
    private int To { set; get; }
    private DateTime Date { set; get; }
    private bool IsRead { set; get; }
    private string Content { set; get; }
}