using SECODashBackend.Models;
using Microsoft.AspNetCore.Mvc;
using SECODashBackend.DataConverters;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Services.Projects;
using Swashbuckle.AspNetCore.Annotations;

namespace SECODashBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ILogger<ProjectsController> _logger;
    private readonly IProjectsService _projectsService;

    public ProjectsController(ILogger<ProjectsController> logger, IProjectsService projectsService)
    {
        _logger = logger;
        _projectsService = projectsService;
    }
   
    [HttpGet]
    [SwaggerOperation("GetAllProjects")]       
    [SwaggerResponse(statusCode: 200, description: "successful operation")]
    public async Task<ActionResult<List<Project>>> GetAllAsync()
    {
        _logger.LogInformation("{Origin}: All projects requested.", this);
        var result = await _projectsService.GetAllAsync(); 
        _logger.LogInformation("{Origin}: Returning all projects.", this);
        return new ObjectResult(result);
    }

    [HttpGet("{id:long}")]
    [SwaggerOperation("GetProjectById")]       
    [SwaggerResponse(statusCode: 200, description: "successful operation")]
    public async Task<ActionResult<Project>> GetByIdAsync(string id)
    {
        _logger.LogInformation("{Origin}: Project requested by Id: '{Ecosystem}'.", this ,id);
        var result = await _projectsService.GetByIdAsync(id); 
        _logger.LogInformation("{Origin}: Returning project with Id: '{Ecosystem}'.", this ,id);
        return result == null ? NotFound() : result;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(Project project)
    {
        _logger.LogInformation("{Origin}: Posting project with the name: '{Ecosystem}'.",
            this, project.Name);
        await _projectsService.AddAsync(project);
        _logger.LogInformation("{Origin}: Project with the name: '{Ecosystem}' has been posted.",
            this, project.Name);

        
        return CreatedAtAction(
            // ReSharper disable once Mvc.ActionNotResolved
            nameof(GetByIdAsync),
            new { id = project.Id },
            project);
    }
}