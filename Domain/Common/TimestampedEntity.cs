namespace Domain.Common;

public abstract class TimestampedEntity : Entity
{
    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }
}
