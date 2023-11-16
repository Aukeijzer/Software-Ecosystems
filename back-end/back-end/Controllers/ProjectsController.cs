using Elastic.Clients.Elasticsearch;
using SECODashBackend.Models;
using Microsoft.AspNetCore.Mvc;
using SECODashBackend.DataConverters;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Services.Projects;
using Swashbuckle.AspNetCore.Annotations;
using System;

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
   
    [HttpGet("{id}")]
    [SwaggerOperation("GetProjectById")]       
    [SwaggerResponse(statusCode: 200, description: "successful operation")]
    public async Task<ActionResult<Project>> GetByIdAsync(string id)
    {
        _logger.LogInformation("{Origin}: Project requested by Id: '{Ecosystem}'.", this ,id);
        var result = await _projectsService.GetByIdAsync(id); 
        _logger.LogInformation("{Origin}: Returning project with Id: '{Ecosystem}'.", this ,id);
        return result == null ? NotFound() : result;
    }
    
    [HttpPost("searchbytopic")]
    [SwaggerOperation("GetProjectsByTopics")]       
    [SwaggerResponse(statusCode: 200, description: "successful operation")]
    public async Task<ActionResult<List<ProjectDto>>> GetByTopicsAsync(List<string> topics)
    {
        _logger.LogInformation("{Origin}: Projects requested with topics: '{topics}'.", this, topics);
        var dtos = await _projectsService.GetByTopicsAsync(topics);
        var projects = dtos.Select(ProjectConverter.ToProjectDto);
        var projectDtos = projects.ToList();
        if (!projectDtos.Any())
        {
            _logger.LogInformation("{Origin}: No projects found with topics: '{topics}'.", this, topics);
            return NotFound();
        }
        _logger.LogInformation("{Origin}: Returning projects with topics: '{topics}'.", this, topics);
        return projectDtos;
    }
    
    [HttpPost("mine")]
    public async Task<ActionResult> MineByTopic(string topic)
    {
        _logger.LogInformation("{Origin}: Mining command received for topic: '{topic}'.", this,topic);
        await _projectsService.MineByTopicAsync(topic);
        return Accepted();
    }
    
    [HttpPost("searchbytimeframe")]
    public async Task<ActionResult<List<ProjectDto>>> GetByTimeFrameAsync(List<DateMath> times)
    {
        DateMath timeFrameMin = times.First();
        DateMath timeFrameMax = times.Last();
        
        _logger.LogInformation("{Origin}: Projects requested with timeFrameMin: '{timeFrameMin}' and timeFrameMax: '{timeFrameMax}'.", this, timeFrameMin, timeFrameMax);
        var dtos = await _projectsService.GetByTimeFrameAsync(timeFrameMin, timeFrameMax);
        var projects = dtos.Select(ProjectConverter.ToProjectDto);
        var projectDtos = projects.ToList();
        if (!projectDtos.Any())
        {
            _logger.LogInformation("{Origin}: No projects found with timeFrameMin: '{timeFrameMin}' and timeFrameMax: '{timeFrameMax}'.", this, timeFrameMin, timeFrameMax);
            return NotFound();
        }
        _logger.LogInformation("{Origin}: Returning projects with timeFrameMin: '{timeFrameMin}' and timeFrameMax: '{timeFrameMax}'.", this, timeFrameMin, timeFrameMax);
        return projectDtos;
    }
}