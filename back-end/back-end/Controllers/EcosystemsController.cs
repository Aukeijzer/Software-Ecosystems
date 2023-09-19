using SECODashBackend.Models;
using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Services;
using SECODashBackend.Services.Ecosystems;
using Swashbuckle.AspNetCore.Annotations;

namespace SECODashBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class EcosystemsController: ControllerBase
{
    private readonly ILogger<EcosystemsController> _logger;
    private readonly IEcosystemsService _ecosystemsService;

    public EcosystemsController(ILogger<EcosystemsController> logger, IEcosystemsService ecosystemsService)
    {
        _logger = logger;
        _ecosystemsService = ecosystemsService;
    }
    [HttpGet]
    [SwaggerOperation("Get all ecosystems")]
    [SwaggerResponse(statusCode: 200, description: "successful operation")]
    public ActionResult<List<Ecosystem>> GetAll(){
        return new ObjectResult(_ecosystemsService.GetAll());
    }

    [HttpGet("{name}")]
    public ActionResult<Ecosystem> GetByName(string name)
    {
        var result = _ecosystemsService.GetByName(name);
        return result == null ? NotFound() : result;
    }
    
    [HttpGet("{id:long}")]
    public ActionResult<Ecosystem> GetById(long id)
    {
        var result = _ecosystemsService.GetById(id);
        return result == null ? NotFound() : result;
    }
    [HttpPost]
    public ActionResult Post(Ecosystem ecosystem)
    {
        _ecosystemsService.Add(ecosystem);
        return CreatedAtAction(
            nameof(GetById),
            new { id = ecosystem.Id },
            ecosystem);
    }
}