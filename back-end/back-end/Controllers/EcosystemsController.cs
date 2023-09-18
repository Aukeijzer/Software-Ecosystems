using SECODashBackend.Models;
using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Services;
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
}