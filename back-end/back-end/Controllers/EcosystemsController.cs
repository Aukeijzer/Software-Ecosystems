using SECODashBackend.Models;
using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Services.Ecosystems;
using Swashbuckle.AspNetCore.Annotations;

namespace SECODashBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class EcosystemsController(ILogger<EcosystemsController> logger, IEcosystemsService ecosystemsService)
    : ControllerBase
{
    /// <summary>
    /// Returns all top-level ecosystems.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [SwaggerOperation("Get all ecosystems")]
    [SwaggerResponse(statusCode: 200, description: "successful operation")]
    public async Task<ActionResult<List<EcosystemOverviewDto>>> GetAllAsync()
    {
        logger.LogInformation("{Origin}: All ecosystems requested.", this);
        var result = await ecosystemsService.GetAllAsync();
        logger.LogInformation("{Origin}: Return all ecosystems.", this);
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
            nameof(SearchByTopics),
            new { id = ecosystem.Id },
            ecosystem);
    }

    [HttpPost]
    public async Task<ActionResult<EcosystemDto>> SearchByTopics(EcosystemRequestDto dto)
    {
        logger.LogInformation("{Origin}: Ecosystem requested with topics: '{topics}'.", this, dto.Topics);
        try
        {
            var ecosystem = await ecosystemsService.GetByTopicsAsync(dto);
            logger.LogInformation("{Origin}: Ecosystem returned with topics: '{topics}'.", this, dto.Topics);
            return ecosystem;
        }
        catch (ArgumentException e)
        {
            logger.LogInformation("{Origin}: No ecosystem returned: '{exception}'.", this, e.Message);
            return BadRequest(e.Message);
        }
        catch (HttpRequestException e)
        {
            logger.LogInformation("{Origin}: No ecosystem returned: '{exception}'.", this, e.Message);
            return Problem(e.Message);
        }
    }
}