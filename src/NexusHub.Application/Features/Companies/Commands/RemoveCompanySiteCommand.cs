using NexusHub.Domain.CompanyContext;

namespace NexusHub.Application.Features.Companies.Commands;

//public record DeleteCompanyRequest(Guid CompanyId) : IRequest;

//public class DeleteCompanyRequestValidator : AbstractValidator<DeleteCompanyRequest>
//{
//    public DeleteCompanyRequestValidator()
//    {
//        RuleFor(x => x.CompanyId).NotEmpty();
//    }
//}

public record RemoveCompanySiteRequest(Guid CompanyId, Guid CompanySiteId) : IRequest;

public class RemoveCompanySiteRequestValidator : AbstractValidator<RemoveCompanySiteRequest>
{
    public RemoveCompanySiteRequestValidator()
    {
        RuleFor(x => x.CompanyId).NotEmpty();
        RuleFor(x => x.CompanySiteId).NotEmpty();
    }
}

public class RemoveCompanySiteCommandHandler : IRequestHandler<RemoveCompanySiteRequest>
{
    private readonly ICompanyRepository _companyRepository;

    public RemoveCompanySiteCommandHandler(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task Handle(RemoveCompanySiteRequest request, CancellationToken cancellationToken)
    {
        var company = await _companyRepository.GetByIdAsync(request.CompanyId) ?? throw new InvalidOperationException($"Company with id {request.CompanyId} not found");

        company.RemoveCompanySite(request.CompanySiteId);

        _companyRepository.Update(company);
    }
}