namespace NexusHub.Infrastructure.Persistence.Entities;

public class ProjectEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public Guid CompanySiteId { get; set; }
}