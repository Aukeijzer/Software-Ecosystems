using SECODashBackend.Models;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<List<Ecosystem>>> GetAllAsync()
    {
        _logger.LogInformation("{Origin}: All ecosystems requested", this);
        var result = await _ecosystemsService.GetAllAsync();
        _logger.LogInformation("{Origin}: Returning all ecosystems", this);
        return new ObjectResult(result);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<Ecosystem>> GetByNameAsync(string name)
    {
        _logger.LogInformation("{Origin}: Ecosystem requested by name: '{Ecosystem}'", this ,name);
        var result = await _ecosystemsService.GetByNameAsync(name);
        _logger.LogInformation("{Origin}: Returning the ecosystem: '{Ecosystem}'", this, name);
        return result == null ? NotFound() : result;
    }
    
    [HttpGet("{id:long}")]
    public async Task<ActionResult<Ecosystem>> GetByIdAsync(long id)
    {   _logger.LogInformation("{Origin}: Ecosystem requested by Id: '{Ecosystem}'", this ,id);
        var result = await _ecosystemsService.GetByIdAsync(id);
        _logger.LogInformation("{Origin}: Returning the ecosystem with Id: '{Ecosystem}'", this ,id);
        return result == null ? NotFound() : result;
    }
    
    [HttpPost]
    public async Task<ActionResult> PostAsync(Ecosystem ecosystem)
    {
        _logger.LogInformation("{Origin}: Posting ecosystem with the name: '{Ecosystem}'", this ,ecosystem.Name);
        await _ecosystemsService.AddAsync(ecosystem);
        _logger.LogInformation("{Origin}: The ecosystem: '{Ecosystem}' has been posted", this ,ecosystem.Name);
        
        return CreatedAtAction(
            // ReSharper disable once Mvc.ActionNotResolved
            nameof(GetByIdAsync),
            new { id = ecosystem.Id },
            ecosystem);
    }
}