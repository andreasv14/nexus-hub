using Microsoft.AspNetCore.Mvc;
using NexusHub.Application.Features.Companies.Commands;
using NexusHub.Application.Features.Companies.Models;
using NexusHub.Application.Features.Companies.Queries;

namespace NexusHub.WebAPI.Controllers;

/// <summary>
/// Companies endpoints
/// </summary>
public class CompaniesController : ApiControllerBase
{
    /// <summary>
    /// Create a new company
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<CompanyDto>> CreateCompany([FromBody] CreateCompanyRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCompany([FromBody] DeleteCompanyRequest request)
    {
        await Mediator.Send(request);

        return Ok();
    }

    [HttpPut]
    public async Task<ActionResult<CompanyDto>> UpdateCompany([FromBody] UpdateCompanyRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpGet("{companyId}")]
    public async Task<CompanyDto?> GetCompany(Guid companyId)
    {
        return await Mediator.Send(new GetCompanyByIdRequest(companyId));
    }

    [HttpGet]
    public async Task<IEnumerable<CompanyDto>> GetCompanies()
    {
        return await Mediator.Send(new GetCompaniesRequest());
    }

    [HttpPost("sites")]
    public async Task<ActionResult<CompanySiteDto>> AddCompanySite([FromBody] AddCompanySiteRequest request)
    {
        return await Mediator.Send(request);
    }

    [HttpDelete("sites")]
    public async Task<IActionResult> RemoveCompanySiteCompany([FromBody] RemoveCompanySiteRequest request)
    {
        await Mediator.Send(request);

        return Ok();
    }
}