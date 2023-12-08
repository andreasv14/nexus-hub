using NexusHub.Domain.CompanyContext.Entities;

namespace NexusHub.Domain.CompanyContext;

public class CompanyAggregate : AggregateRoot<Guid>
{
    private readonly List<CompanySite> _companySites = new();

    private CompanyAggregate(
        Guid id,
        string name,
        string description) : base(id)
    {
        Name = name;
        Description = description;

        CreateDefaultCompanySite(name, description);
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public IReadOnlyCollection<CompanySite> CompanySites => _companySites;

    public void AddCompanySite(CompanySite newCompanySite)
    {
        if (newCompanySite.IsMainBranch)
        {
            var previousMainBranch = _companySites.Single(x => x.IsMainBranch);
            previousMainBranch.UpdateMainBranch(false);
        }

        _companySites.Add(newCompanySite);
    }

    public void RemoveCompanySite(Guid companySiteId)
    {
        var companySite = _companySites.FirstOrDefault(x => x.Id == companySiteId);
        if (companySite == null)
        {
            return;
        }

        if (companySite.IsMainBranch)
        {
            throw new InvalidOperationException("Cannot remove main branch");
        }

        _companySites.Remove(companySite);
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static CompanyAggregate Create(string name, string description)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException("Company name cannot be null or empty");
        }

        return new CompanyAggregate(
            id: Guid.NewGuid(),
            name,
            description);
    }

    private void CreateDefaultCompanySite(string name, string description)
    {
        if (CompanySites.Any()) return;
        _companySites.Add(CompanySite.Create(name, description, isMainBranch: true));
    }
}