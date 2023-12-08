using NexusHub.Application.Features.Companies.Models;
using NexusHub.Application.Services;

namespace NexusHub.Application.Features.Companies.Commands;

public record UpdateCompanyRequest(
    Guid CompanyId,
    string Name,
    string Description) : IRequest<CompanyDto>;

public class UpdateCompanyRequestValidator : AbstractValidator<UpdateCompanyRequest>
{
    public UpdateCompanyRequestValidator()
    {
        RuleFor(x => x.CompanyId)
            .NotEmpty();

        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Description)
            .MaximumLength(200);
    }
}

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyRequest, CompanyDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCompanyCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CompanyDto> Handle(UpdateCompanyRequest request, CancellationToken cancellationToken)
    {
        var company = await _unitOfWork.Companies.GetByIdAsync(request.CompanyId);
        if (company == null)
        {
            throw new InvalidOperationException($"Company with name {request.Name} already exists");
        }

        company.Update(request.Name, request.Description);

        _unitOfWork.Companies.Update(company);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CompanyDto>(company);
    }
}