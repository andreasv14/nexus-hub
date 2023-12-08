namespace NexusHub.Domain.CompanyContext.Entities;

public class CompanySite : Entity<Guid>
{
    private readonly List<Guid> _projectIds = new();

    public CompanySite(Guid id, string name, string description, bool isMainBranch) : base(id)
    {
        Name = name;
        Description = description;
        IsMainBranch = isMainBranch;
    }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public bool IsMainBranch { get; private set; }

    public IReadOnlyCollection<Guid> ProjectIds => _projectIds;

    public void UpdateMainBranch(bool isMainBranch)
    {
        IsMainBranch = isMainBranch;
    }

    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public void AddProject(Guid projectId)
    {
        if (_projectIds.Contains(projectId)) return;

        _projectIds.Add(projectId);
    }

    public void RemoveProject(Guid projectId)
    {
        if (_projectIds.Contains(projectId)) return;

        _projectIds.Remove(projectId);
    }

    public static CompanySite Create(string name, string description, bool isMainBranch)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));

        if (description == null) throw new ArgumentNullException(nameof(description));

        return new CompanySite(
            id: Guid.NewGuid(),
            name,
            description,
            isMainBranch);
    }
}