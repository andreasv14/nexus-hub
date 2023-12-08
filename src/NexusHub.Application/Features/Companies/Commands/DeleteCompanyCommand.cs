using NexusHub.Domain.CompanyContext;

namespace NexusHub.Application.Features.Companies.Commands;

public record DeleteCompanyRequest(Guid CompanyId) : IRequest;

public class DeleteCompanyRequestValidator : AbstractValidator<DeleteCompanyRequest>
{
    public DeleteCompanyRequestValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty();
    }
}

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyRequest>
{
    private readonly ICompanyRepository _companyRepository;

    public DeleteCompanyCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task Handle(DeleteCompanyRequest request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(request.CompanyId) ?? throw new InvalidOperationException($"Company with id {request.CompanyId} not found");

        _companyRepository.Delete(company.Id);
    }
}