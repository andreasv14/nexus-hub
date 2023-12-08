using NexusHub.Application.Features.Companies.Models;
using NexusHub.Domain.CompanyContext;
using NexusHub.Domain.CompanyContext.Entities;

namespace NexusHub.Application.Features.Companies.Commands;

public record AddCompanySiteRequest(
    Guid CompanyId,
    string Name,
    string Description,
    bool IsMainBranch) : IRequest<CompanySiteDto>;

public class AddCompanySiteRequestValidator : AbstractValidator<AddCompanySiteRequest>
{
    public AddCompanySiteRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(200);

        RuleFor(x => x.IsMainBranch)
            .NotEmpty();
    }
}

public class AddCompanySiteCommandHandler : IRequestHandler<AddCompanySiteRequest, CompanySiteDto>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public AddCompanySiteCommandHandler(
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<CompanySiteDto> Handle(AddCompanySiteRequest request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(request.CompanyId) ?? throw new InvalidOperationException($"Company with id {request.CompanyId} not found");
        var newSite = CompanySite.Create(request.Name, request.Description, request.IsMainBranch);
        company.AddCompanySite(newSite);

        _companyRepository.Update(company);

        return _mapper.Map<CompanySiteDto>(newSite);
    }
}