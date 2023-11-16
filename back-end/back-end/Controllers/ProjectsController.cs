﻿using SECODashBackend.Models;
using Microsoft.AspNetCore.Mvc;
using SECODashBackend.DataConverters;
using SECODashBackend.Dtos.Project;
using SECODashBackend.Services.Projects;
using Swashbuckle.AspNetCore.Annotations;

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
    public async Task<ActionResult<List<ProjectDto>>> GetByTimeFrameAsync(DateTime time)
    {
        _logger.LogInformation("{Origin}: Projects requested with time: '{time}' '.", this, time);
        var dtos = await _projectsService.GetByTimeFrameAsync(time);
        var projects = dtos.Select(ProjectConverter.ToProjectDto);
        var projectDtos = projects.ToList();
        if (!projectDtos.Any())
        {
            _logger.LogInformation("{Origin}: No projects found with time: '{time}' '.", this, time);
            return NotFound();
        }
        _logger.LogInformation("{Origin}: Returning projects with time: '{time}' '.", this, time);
        return projectDtos;
    }
}