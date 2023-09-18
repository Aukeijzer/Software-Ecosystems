using SECODashBackend.Models;
using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Services;
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
    public ActionResult<List<Project>> GetAll(){
        
        return new ObjectResult(_projectsService.GetAll());
    }
}