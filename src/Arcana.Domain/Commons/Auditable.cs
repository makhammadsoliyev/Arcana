namespace Arcana.Domain.Commons;

public abstract class Auditable
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public long CreatedByUserId { get; set; }
    public long? UpdatedByUserId { get; set; }
}
