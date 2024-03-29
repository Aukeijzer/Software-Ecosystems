﻿// Copyright (C) <2024>  <ODINDash>
// 
// This file is part of SECODash.
// 
// SECODash is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// SECODash is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
// 
// You should have received a copy of the GNU Affero General Public License
// along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

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
    /// Returns the technology taxonomy of the given ecosystem.
    /// This list was saved in the postgres database when the ecosystem was created.
    /// </summary>
    /// <param name="ecosystemName">The name of the ecosystem you want to find technologies for.</param>
    /// <returns>A list of technologies of the given ecosystem.</returns>
    [HttpGet("taxonomy/technologies")]
    public async Task<ActionResult<List<string>>> GetTechnologyTaxonomy(string ecosystemName)
    {
        logger.LogInformation("{Origin}: Technology taxonomy of ecosystem '{ecosystemName}' requested.", this, ecosystemName);
        var result = await ecosystemsService.GetTechnologyTaxonomy(ecosystemName);
        logger.LogInformation("{Origin}: Return technology taxonomy of ecosystem '{ecosystemName}'.", this, ecosystemName);
        return new ObjectResult(result);
    }

    /// <summary>
    /// Returns an ecosystem defined by the topics in the dto.
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
    /// <summary>
    /// Update the description of a top-level ecosystem.
    /// </summary>
    [HttpPost("DescriptionUpdate")]
    [SwaggerOperation("Updates description for root ecosystem")]
    [SwaggerResponse(statusCode: 200, description: "successfully updated description")]
    public async Task<ActionResult<DescriptionDto>> UpdateDescription(DescriptionRequestDto req)
    {
        logger.LogInformation("{Origin}: Updating ecosystem description.",this);
        try
        {
            var result = await ecosystemsService.UpdateDescription(req);
            var response = new DescriptionDto()
            {
                Description = result
            };
            logger.LogInformation("{Origin}: Successfully updated the description.",this);
            return new ObjectResult(response);
        }
        catch (Exception e)
        {
            logger.LogInformation("{Origin}: Failed to update ecosystem description: '{exception}'.",this,e.Message);
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// Create a new ecosystem from provided information.
    /// </summary>
    /// <param name="dto">All information needed to create a new ecosystem </param>
    [HttpPost("CreateEcosystem")]
    [SwaggerOperation("Create a new ecosystem")]
    [SwaggerResponse(statusCode: 200, description: "successfully created the ecosystem.")]
    public async Task<string> CreateEcosystem(EcosystemCreationDto dto)
    {
        logger.LogInformation("{Origin}: Trying to create the {ecosystem} ecosystem.", this, dto.EcosystemName);
        var create = await ecosystemsService.CreateEcosystem(dto);
        if (!create)
        {
            logger.LogInformation("{Origin}: The {ecosystem} ecosystem already exists.", this, dto.EcosystemName);
            return "Ecosystem already exists.";
        }
        var update = await ecosystemsService.UpdateTopics(dto);
        logger.LogInformation("{Origin}: Successfully updated the topics for {ecosystem}.",this,dto.EcosystemName);
        await ecosystemsService.ScheduleEcosystem(dto.EcosystemName);
        return "Ecosystem created";
    }

    [HttpPost("RemoveEcosystem")]
    [SwaggerOperation("Remove an existing ecosystem")]
    [SwaggerResponse(statusCode: 200, description: "Successfully removed the ecosystem")]
    public async Task<string> RemoveEcosystem(RemoveEcosystemDto dto)
    { 
        var result = await ecosystemsService.RemoveEcosystem(dto.Ecosystem);
        logger.LogInformation("{Origin}:" + result ,this);
        return result;
    }
}