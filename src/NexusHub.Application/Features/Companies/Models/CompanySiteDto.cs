namespace NexusHub.Application.Features.Companies.Models;

public record CompanySiteDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public bool IsMainBranch { get; set; }
}