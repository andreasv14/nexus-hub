using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NexusHub.Domain.CompanyContext;
using NexusHub.Domain.CompanyContext.Entities;
using NexusHub.Infrastructure.Persistence.Entities;

namespace NexusHub.Infrastructure.Persistence.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public CompanyRepository(
        ApplicationDbContext dbContext,
        IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<CompanyAggregate> AddAsync(CompanyAggregate company, CancellationToken cancellationToken = default)
    {
        await _dbContext.Companies.AddAsync(_mapper.Map<CompanyEntity>(company));

        await _dbContext.SaveChangesAsync(cancellationToken);

        return company;
    }

    public async Task<CompanyAggregate?> GetByIdAsync(Guid id)
    {
        return _mapper.Map<CompanyAggregate>(
            await _dbContext.Companies.AsNoTracking()
                .Include(x => x.CompanySites).FirstOrDefaultAsync(x => x.Id == id));
    }

    public void Update(CompanyAggregate company)
    {
        _dbContext.Companies.Update(_mapper.Map<CompanyEntity>(company));

        _dbContext.SaveChanges();
    }

    public async Task<CompanyAggregate?> GetByNameAsync(string name)
    {
        return _mapper.Map<CompanyAggregate>(await _dbContext.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.Name == name));
    }

    public async Task<IEnumerable<CompanyAggregate>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<CompanyAggregate>>(await _dbContext.Companies.AsNoTracking().Include(x => x.CompanySites).ToListAsync());
    }

    public async Task<CompanySite?> GetCompanySiteByIdAsync(Guid companySiteId)
    {
        return _mapper.Map<CompanySite>(await _dbContext.CompanySites.FindAsync(companySiteId));
    }

    public void Delete(Guid companyId)
    {
        var company = _dbContext.Companies.Find(companyId);
        if (company == null)
        {
            return;
        }

        _dbContext.Companies.Remove(company);

        _dbContext.SaveChanges();
    }
}