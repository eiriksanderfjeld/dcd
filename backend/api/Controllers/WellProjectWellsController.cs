
using api.Dtos;
using api.Services;

using Api.Authorization;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

namespace api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
[RequiresApplicationRoles(
        ApplicationRole.Admin,
        ApplicationRole.ReadOnly,
        ApplicationRole.User

    )]
public class WellProjectWellsController : ControllerBase
{

    private readonly IWellProjectWellService _wellProjectWellService;

    public WellProjectWellsController(IWellProjectWellService wellProjectWellService)
    {
        _wellProjectWellService = wellProjectWellService;
    }

    [HttpGet(Name = "GetWellProjectWells")]
    public IEnumerable<WellProjectWellDto> GetWellProjectWells([FromQuery] Guid projectId)
    {
        if (projectId != Guid.Empty)
        {
            // return _wellProjectWellService.GetDtosForProject(projectId);
        }
        return _wellProjectWellService.GetAllDtos();
    }

    [HttpPost(Name = "CreateWellProjectWell")]
    public ProjectDto CreateWellProjectWell([FromBody] WellProjectWellDto wellDto)
    {
        return _wellProjectWellService.CreateWellProjectWell(wellDto);
    }

    [HttpPut(Name = "UpdateWellProjectWell")]
    public ProjectDto UpdateWellProjectWell([FromBody] WellProjectWellDto wellDto)
    {
        return _wellProjectWellService.UpdateWellProjectWell(wellDto);
    }

    [HttpPost("multiple", Name = "CreateMultipleWellProjectWells")]
    public WellProjectWellDto[]? CreateMultipleWellProjectWells([FromBody] WellProjectWellDto[] wellDto)
    {
        return _wellProjectWellService.CreateMultipleWellProjectWells(wellDto);
    }

    [HttpPut("multiple", Name = "UpdateMultipleWellProjectWells")]
    public WellProjectWellDto[]? UpdateMultipleWellProjectWells([FromQuery] Guid caseId, [FromBody] WellProjectWellDto[] wellDto)
    {
        return _wellProjectWellService.UpdateMultipleWellProjectWells(wellDto, caseId);
    }
}
