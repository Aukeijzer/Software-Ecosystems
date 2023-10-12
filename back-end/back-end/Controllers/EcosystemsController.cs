using SECODashBackend.Models;
using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Dto;
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
        var result = await _ecosystemsService.GetAllAsync();
        return new ObjectResult(result);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<Ecosystem>> GetByNameAsync(string name)
    {
        var result = await _ecosystemsService.GetByNameAsync(name);
        return result == null ? NotFound() : result;
    }
    
    [HttpGet("id/{id}")]
    public async Task<ActionResult<Ecosystem>> GetByIdAsync(string id)
    {
        var result = await _ecosystemsService.GetByIdAsync(id);
        return result == null ? NotFound() : result;
    }
    
    [HttpPost]
    public async Task<ActionResult> PostAsync(Ecosystem ecosystem)
    {
        await _ecosystemsService.AddAsync(ecosystem);
        
        return CreatedAtAction(
            // ReSharper disable once Mvc.ActionNotResolved
            nameof(GetByIdAsync),
            new { id = ecosystem.Id },
            ecosystem);
    }
}