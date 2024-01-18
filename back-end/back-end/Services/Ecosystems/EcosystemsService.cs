using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.DataConverters;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Models;
using SECODashBackend.Services.Analysis;

namespace SECODashBackend.Services.Ecosystems;
    
/// <summary>
/// This service is responsible for handling all ecosystem-related requests.
/// It uses the EcosystemsContext to interact with the database.
/// It uses the AnalysisService to analyze ecosystems.
/// </summary>
public class EcosystemsService(EcosystemsContext dbContext,
        IAnalysisService analysisService)
    : IEcosystemsService
{
    private const int DefaultNumberOfTopItems = 10;

    /// <summary>
    /// Get all top-level ecosystems, i.e., Agriculture, Quantum, Artificial Intelligence.
    /// </summary>
    public async Task<List<EcosystemOverviewDto>> GetAllAsync()
    {
        var ecosystems = await dbContext.Ecosystems
            .AsNoTracking()
            .ToListAsync();
        return ecosystems.Select(EcosystemConverter.ToDto).ToList();
    }

    /// <summary>
    /// Get an ecosystem by its name.
    /// </summary>
    /// <param name="name">The name of the ecosystem to get.</param>
    /// <returns>The ecosystem.</returns>
    private async Task<Ecosystem?> GetByNameAsync(string name)
    {
        return await dbContext.Ecosystems
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Name == name);
    }

    /// <summary>
    /// Get an ecosystem by its topics.
    /// </summary>
    /// <param name="dto">The Dto that contains the request information of the ecosystem to get.</param>
    /// <returns>The ecosystem.</returns>
    public async Task<EcosystemDto> GetByTopicsAsync(EcosystemRequestDto dto)
    {
        if (dto.Topics.Count == 0) throw new ArgumentException("Number of topics cannot be 0");
        
        // Retrieve the technology taxonomy from the database that are in the ecosystem
        var technologyTaxonomy = await dbContext.Ecosystems
            .AsNoTracking()
            .Include(e => e.Technologies)
            .ToListAsync();
        
        var ecosystemDto = await analysisService.AnalyzeEcosystemAsync(
            dto.Topics,
            [],
            dto.NumberOfTopLanguages ?? DefaultNumberOfTopItems,
            dto.NumberOfTopSubEcosystems ?? DefaultNumberOfTopItems,
            dto.NumberOfTopContributors ?? DefaultNumberOfTopItems,
            dto.NumberOfTopTechnologies ?? DefaultNumberOfTopItems,
            dto.NumberOfTopProjects ?? DefaultNumberOfTopItems,
            dto.StartTime,
            dto.EndTime,
            dto.TimeBucket);

        // If the ecosystem has more than 1 topic, we know it is not one of the "main" ecosystems
        if (dto.Topics.Count != 1) return ecosystemDto;
            
        // Check if the database has additional data regarding this ecosystem
        var ecosystem = await GetByNameAsync(dto.Topics.First());

        // If it doesn't, return the dto as is, else add the additional data
        if (ecosystem == null) return ecosystemDto;
        ecosystemDto.DisplayName = ecosystem.DisplayName;
        ecosystemDto.NumberOfStars = ecosystem.NumberOfStars;
        ecosystemDto.Description = ecosystem.Description;
        
        return ecosystemDto;
    }
    /// <summary>
    /// Update description for ecosystem with given description
    /// </summary>
    /// <param name="dto">The <see cref="DescriptionDto"/> containts the new description to be saved to the database.</param>
    /// <returns>Returns a <see cref="string"/> with the update status.</returns>
    public async Task<string> UpdateDescription(DescriptionRequestDto dto)
    {
        
        //To lower is because all names are without capital letters
        var ecosystemToUpdate = dbContext.Ecosystems.FirstOrDefault(ecosystem => ecosystem.Name == dto.Ecosystem.ToLower());
        if (ecosystemToUpdate != null)
        {
            ecosystemToUpdate.Description = dto.Description;
            await dbContext.SaveChangesAsync();
            return "updated successfully";
        }
        else
        {
            throw new Exception("Ecosystem not found");

        }
    }
    
    public async Task<List<string>> GetTechnologyTaxonomy(string ecosystemName)
    {
        var ecosystem = await GetByNameAsync(ecosystemName);
        if (ecosystem == null) throw new ArgumentException("Ecosystem not found");
        return ecosystem.Technologies;
    }
}