namespace NexusHub.Infrastructure.Persistence.Entities;

public class CompanyEntity
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<CompanySiteEntity> CompanySites { get; set; } = new HashSet<CompanySiteEntity>();
}