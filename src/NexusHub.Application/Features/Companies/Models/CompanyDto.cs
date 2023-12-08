namespace NexusHub.Application.Features.Companies.Models;

public record CompanyDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public ICollection<CompanySiteDto> CompanySites { get; set; } = new List<CompanySiteDto>();
}