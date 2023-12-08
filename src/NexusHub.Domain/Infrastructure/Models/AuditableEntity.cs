namespace NexusHub.Domain.Infrastructure.Models;

public abstract class AuditableEntity
{
    public Guid CreatedBy { get; set; }
}
