namespace Entities.Abstractions;

public class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public int Sort { get; set; } = 500;
    public bool Active { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public Guid CreatedBy { get; set; }
    public Guid UpdatedBy { get; set; }
    public bool Deleted { get; set; }
}