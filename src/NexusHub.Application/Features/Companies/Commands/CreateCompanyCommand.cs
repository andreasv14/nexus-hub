using NexusHub.Application.Features.Companies.Models;
using NexusHub.Domain.CompanyContext;

namespace NexusHub.Application.Features.Companies.Commands;

public record CreateCompanyRequest(
    string Name,
    string Description) : IRequest<CompanyDto>;

public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
{
    public CreateCompanyRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(200);
    }
}

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyRequest, CompanyDto>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public CreateCompanyCommandHandler(
        ICompanyRepository companyRepository,
        IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<CompanyDto> Handle(CreateCompanyRequest request, CancellationToken cancellationToken)
    {
        var existingCompany = await _companyRepository.GetByNameAsync(request.Name);
        if (existingCompany != null)
        {
            throw new InvalidOperationException($"Company with name {request.Name} already exists");
        }

        var newCompany = CompanyAggregate.Create(request.Name, request.Description);

        await _companyRepository.AddAsync(newCompany);

        return _mapper.Map<CompanyDto>(newCompany);
    }
}