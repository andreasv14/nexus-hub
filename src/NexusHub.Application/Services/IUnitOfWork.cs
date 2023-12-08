using NexusHub.Domain.CompanyContext;

namespace NexusHub.Application.Services;

public interface IUnitOfWork
{
    public ICompanyRepository Companies { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
