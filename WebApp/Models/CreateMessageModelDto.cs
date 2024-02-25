namespace WebApp.Models;

public class CreateMessageModelDto
{
    public Guid To { set; get; }
    public string Content { set; get; }
    public string Files { set; get; }
}