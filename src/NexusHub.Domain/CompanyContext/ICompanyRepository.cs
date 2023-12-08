using NexusHub.Domain.CompanyContext.Entities;

namespace NexusHub.Domain.CompanyContext;

public interface ICompanyRepository
{
    Task<CompanyAggregate> AddAsync(CompanyAggregate company, CancellationToken cancellationToken = default);

    void Update(CompanyAggregate company);

    Task<CompanyAggregate?> GetByIdAsync(Guid id);

    Task<CompanySite?> GetCompanySiteByIdAsync(Guid companySiteId);

    Task<CompanyAggregate?> GetByNameAsync(string name);

    Task<IEnumerable<CompanyAggregate>> GetAllAsync();

    void Delete(Guid companyId);
}