using NexusHub.Application.Features.Companies.Models;
using NexusHub.Domain.CompanyContext;

namespace NexusHub.Application.Features.Companies.Queries;

public record GetCompanyByIdRequest(Guid CompanyId) : IRequest<CompanyDto?>;

public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdRequest, CompanyDto?>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public GetCompanyByIdQueryHandler(
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<CompanyDto?> Handle(GetCompanyByIdRequest request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(request.CompanyId);

        return _mapper.Map<CompanyDto>(company);
    }
}