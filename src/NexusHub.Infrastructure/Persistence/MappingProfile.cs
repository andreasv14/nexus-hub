using AutoMapper;
using NexusHub.Domain.CompanyContext;
using NexusHub.Domain.CompanyContext.Entities;
using NexusHub.Infrastructure.Persistence.Entities;

namespace NexusHub.Infrastructure.Persistence;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CompanyEntity, CompanyAggregate>().ReverseMap();
        CreateMap<CompanySiteEntity, CompanySite>().ReverseMap();
    }
}