using SECODashBackend.Models;
using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Dtos.Ecosystem;
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
        _logger.LogInformation("{Origin}: All ecosystems requested.", this);
        var result = await _ecosystemsService.GetAllAsync();
        _logger.LogInformation("{Origin}: Return all ecosystems.", this);
        return new ObjectResult(result);
    }

    [HttpPost("add")]
    // Currently not in use
    // TODO: convert to accept a dto instead of an Ecosystem
    public async Task<ActionResult> PostAsync(Ecosystem ecosystem)
    {
        _logger.LogInformation("{Origin}: Posting ecosystem with the name: '{Ecosystem}'", this,
            ecosystem.Name);
        await _ecosystemsService.AddAsync(ecosystem);
        _logger.LogInformation("{Origin}: Ecosystem with the name: '{Ecosystem}' has been posted.",
            this, ecosystem.Name);
        
        return CreatedAtAction(
            // ReSharper disable once Mvc.ActionNotResolved
            nameof(GetByIdAsync),
            new { id = ecosystem.Id },
            ecosystem);
    }
}