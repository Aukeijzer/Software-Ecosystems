using SECODashBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace SECODashBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(ILogger<ProjectsController> logger)
    {
        _logger = logger;
    }
   
    [HttpGet]
    [SwaggerOperation("GetAllProjects")]       
    [SwaggerResponse(statusCode: 200, description: "successful operation")]
    public ActionResult<List<Project>> GetAll(){
        
        var example = new List<Project>{new("awesome-agriculture", 1, "Open source technology for agriculture, farming, and gardening", null, null, 1100)};
        return new ObjectResult(example);
    }
}