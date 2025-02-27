using api.Adapters;
using api.Context;
using api.Dtos;
using api.Models;

using Microsoft.EntityFrameworkCore;

namespace api.Services;

public class TopsideService : ITopsideService
{
    private readonly DcdDbContext _context;
    private readonly IProjectService _projectService;
    private readonly ILogger<TopsideService> _logger;

    public TopsideService(DcdDbContext context, IProjectService projectService, ILoggerFactory loggerFactory)
    {
        _context = context;
        _projectService = projectService;
        _logger = loggerFactory.CreateLogger<TopsideService>();
    }

    public TopsideDto CopyTopside(Guid topsideId, Guid sourceCaseId)
    {
        var source = GetTopside(topsideId);
        var newTopsideDto = TopsideDtoAdapter.Convert(source);
        newTopsideDto.Id = Guid.Empty;
        if (newTopsideDto.CostProfile != null)
        {
            newTopsideDto.CostProfile.Id = Guid.Empty;
        }
        if (newTopsideDto.CostProfileOverride != null)
        {
            newTopsideDto.CostProfileOverride.Id = Guid.Empty;
        }
        if (newTopsideDto.CessationCostProfile != null)
        {
            newTopsideDto.CessationCostProfile.Id = Guid.Empty;
        }

        var topside = NewCreateTopside(newTopsideDto, sourceCaseId);
        var dto = TopsideDtoAdapter.Convert(topside);

        return dto;
    }

    public ProjectDto CreateTopside(TopsideDto topsideDto, Guid sourceCaseId)
    {
        var topside = TopsideAdapter.Convert(topsideDto);
        var project = _projectService.GetProject(topsideDto.ProjectId);
        topside.Project = project;
        topside.LastChangedDate = DateTimeOffset.UtcNow;
        topside.ProspVersion = topsideDto.ProspVersion;
        _context.Topsides!.Add(topside);
        _context.SaveChanges();
        SetCaseLink(topside, sourceCaseId, project);
        return _projectService.GetProjectDto(project.Id);
    }

    public Topside NewCreateTopside(TopsideDto topsideDto, Guid sourceCaseId)
    {
        var topside = TopsideAdapter.Convert(topsideDto);
        var project = _projectService.GetProject(topsideDto.ProjectId);
        topside.Project = project;
        topside.LastChangedDate = DateTimeOffset.UtcNow;
        topside.ProspVersion = topsideDto.ProspVersion;
        var createdTopside = _context.Topsides!.Add(topside);
        _context.SaveChanges();
        SetCaseLink(topside, sourceCaseId, project);
        return createdTopside.Entity;
    }

    private void SetCaseLink(Topside topside, Guid sourceCaseId, Project project)
    {
        var case_ = project.Cases!.FirstOrDefault(o => o.Id == sourceCaseId);
        if (case_ == null)
        {
            throw new NotFoundInDBException(string.Format("Case {0} not found in database.", sourceCaseId));
        }
        case_.TopsideLink = topside.Id;
        _context.SaveChanges();
    }

    public ProjectDto DeleteTopside(Guid topsideId)
    {
        var topside = GetTopside(topsideId);
        _context.Topsides!.Remove(topside);
        DeleteCaseLinks(topsideId);
        _context.SaveChanges();
        return _projectService.GetProjectDto(topside.ProjectId);
    }

    private void DeleteCaseLinks(Guid topsideId)
    {
        foreach (Case c in _context.Cases!)
        {
            if (c.TopsideLink == topsideId)
            {
                c.TopsideLink = Guid.Empty;
            }
        }
    }

    public ProjectDto UpdateTopside(TopsideDto updatedTopsideDto)
    {
        var existing = GetTopside(updatedTopsideDto.Id);
        TopsideAdapter.ConvertExisting(existing, updatedTopsideDto);

        existing.LastChangedDate = DateTimeOffset.UtcNow;
        _context.Topsides!.Update(existing);
        _context.SaveChanges();
        return _projectService.GetProjectDto(updatedTopsideDto.ProjectId);
    }

    public TopsideDto NewUpdateTopside(TopsideDto updatedTopsideDto)
    {
        var existing = GetTopside(updatedTopsideDto.Id);
        TopsideAdapter.ConvertExisting(existing, updatedTopsideDto);

        existing.LastChangedDate = DateTimeOffset.UtcNow;
        var updatedTopside = _context.Topsides!.Update(existing);
        _context.SaveChanges();
        return TopsideDtoAdapter.Convert(updatedTopside.Entity);
    }

    public Topside GetTopside(Guid topsideId)
    {
        var topside = _context.Topsides!
            .Include(c => c.Project)
            .Include(c => c.CostProfile)
            .Include(c => c.CostProfileOverride)
            .Include(c => c.CessationCostProfile)
            .FirstOrDefault(o => o.Id == topsideId);
        if (topside == null)
        {
            throw new ArgumentException(string.Format("Topside {0} not found.", topsideId));
        }
        return topside;
    }
}
