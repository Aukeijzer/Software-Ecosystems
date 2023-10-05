using SECODashBackend.Models;
using Microsoft.AspNetCore.Mvc;
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
        var result = await _projectsService.GetAllAsync(); 
        return new ObjectResult(result);
    }

    [HttpGet("{id:long}")]
    [SwaggerOperation("GetProjectById")]       
    [SwaggerResponse(statusCode: 200, description: "successful operation")]
    public async Task<ActionResult<Project>> GetByIdAsync(string id)
    {
        var result = await _projectsService.GetByIdAsync(id); 
        return result == null ? NotFound() : result;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync(Project project)
    {
        await _projectsService.AddAsync(project);
        return CreatedAtAction(
            // ReSharper disable once Mvc.ActionNotResolved
            nameof(GetByIdAsync),
            new { id = project.Id },
            project);
    }
}