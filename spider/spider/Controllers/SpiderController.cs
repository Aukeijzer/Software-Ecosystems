using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SECODashBackend.Models;
using spider.Converter;
using spider.Services;

namespace spider.Controllers;

[ApiController]
[Route("[controller]")]
public class SpiderController : ControllerBase
{
    private readonly IGitHubService _gitHubService;
    private readonly ISpiderDataConverter _spiderDataConverter;
    private readonly ILogger<SpiderController> _logger;
    

    public SpiderController(IGitHubService gitHubService, ISpiderDataConverter spiderDataConverter, ILogger<SpiderController> logger)
    {
        _logger = logger;
        _gitHubService = gitHubService;
        _spiderDataConverter = spiderDataConverter;
    }
    //http:localhost:Portnumberhere/spider/name
    [HttpGet("{name}")]
    public async Task<ActionResult<List<Project>>> GetByTopic(string name)
    {
        _logger.LogInformation("{Origin}: A request has been made to mine for the {Ecosystem} ecosystem", this, name);
        var response = _spiderDataConverter.ToProjects(await _gitHubService.QueryRepositoriesByName(name));
        _logger.LogInformation("{Origin}: Returning the mined results for the requested ecosystem: '{Ecosystem}'", this ,name);
        return response;
    }
}