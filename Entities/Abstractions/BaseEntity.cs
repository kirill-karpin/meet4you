namespace Entities.Abstractions;

public class BaseEntity
{
    public Guid Id { get; set; }
    public int Sort { get; set; }
    public bool Active { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
}