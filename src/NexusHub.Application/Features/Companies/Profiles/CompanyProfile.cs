using NexusHub.Application.Features.Companies.Models;
using NexusHub.Domain.CompanyContext;
using NexusHub.Domain.CompanyContext.Entities;

namespace NexusHub.Application.Features.Companies.Profiles;

public class CompanyProfile : Profile
{
    public CompanyProfile()
    {
        CreateMap<CompanyAggregate, CompanyDto>()
            .ReverseMap()
            .ConstructUsing(x => CompanyAggregate.Create(x.Name, x.Description));

        CreateMap<CompanySite, CompanySiteDto>()
            .ReverseMap()
            .ConstructUsing(x => CompanySite.Create(x.Name, x.Description, x.IsMainBranch));
    }
}