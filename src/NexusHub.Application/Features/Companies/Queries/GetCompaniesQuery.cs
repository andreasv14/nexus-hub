using NexusHub.Application.Features.Companies.Models;
using NexusHub.Domain.CompanyContext;

namespace NexusHub.Application.Features.Companies.Queries;

public record GetCompaniesRequest() : IRequest<IEnumerable<CompanyDto>>;

public class GetCompaniesQueryHandler : IRequestHandler<GetCompaniesRequest, IEnumerable<CompanyDto>>
{
    private readonly ICompanyRepository companyRepository;
    private readonly IMapper mapper;

    public GetCompaniesQueryHandler(
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        this.companyRepository = companyRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<CompanyDto>> Handle(GetCompaniesRequest request, CancellationToken cancellationToken)
    {
        return mapper.Map<IEnumerable<CompanyDto>>(await companyRepository.GetAllAsync());
    }
}