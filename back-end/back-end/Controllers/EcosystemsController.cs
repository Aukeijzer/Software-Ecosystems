using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Services.Ecosystems;
using Swashbuckle.AspNetCore.Annotations;

namespace SECODashBackend.Controllers;
/// <summary>
/// This controller is responsible for handling requests related to ecosystems.
/// </summary>
[ApiController]
[Route("[controller]")]
public class EcosystemsController(ILogger<EcosystemsController> logger, IEcosystemsService ecosystemsService)
    : ControllerBase
{
    /// <summary>
    /// Returns all top-level ecosystems.
    /// </summary>
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

    /// <summary>
    /// Returns an ecosystem defined by the topics in the dto
    /// </summary>
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
    
    [HttpPost("DescriptionUpdate")]
    [SwaggerOperation("Updates description for root ecosystem")]
    [SwaggerResponse(statusCode: 200, description: "successfully updated description")]
    public async Task<ActionResult<descriptionDto>> UpdateDescription(descriptionRequestDto req)
    {
        try
        {
            var result = await ecosystemsService.UpdateDescription(req);
            var response = new descriptionDto()
            {
                Description = result
            };
            return new ObjectResult(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
}