namespace NexusHub.Infrastructure.Persistence.Entities;

public class CompanySiteEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsMainBranch { get; set; }

    public Guid CompanyId { get; set; }
    public virtual CompanyEntity Company { get; set; } = null!;
}