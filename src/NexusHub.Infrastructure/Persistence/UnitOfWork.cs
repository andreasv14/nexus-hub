using NexusHub.Application.Services;
using NexusHub.Domain.CompanyContext;

namespace NexusHub.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(
        ApplicationDbContext dbContext,
        ICompanyRepository companyRepository)
    {
        _dbContext = dbContext;
        Companies = companyRepository;
    }

    public ICompanyRepository Companies { get; }


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }
}