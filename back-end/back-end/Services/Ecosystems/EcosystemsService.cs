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
    private async Task<Ecosystem?> GetByNameAsync(string name)
    {
        return await dbContext.Ecosystems
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Name == name);
    }

    /// <summary>
    /// Get an ecosystem by its topics.
    /// </summary>
    public async Task<EcosystemDto> GetByTopicsAsync(EcosystemRequestDto dto)
    {
        if (dto.Topics.Count == 0) throw new ArgumentException("Number of topics cannot be 0");

        var ecosystemDto = await analysisService.AnalyzeEcosystemAsync(
            dto.Topics,
            dto.NumberOfTopLanguages ?? DefaultNumberOfTopItems,
            dto.NumberOfTopSubEcosystems ?? DefaultNumberOfTopItems,
            dto.NumberOfTopContributors ?? DefaultNumberOfTopItems);

        // Check if the database has additional data regarding this ecosystem
        var ecosystem = await GetByNameAsync(dto.Topics.First());

        // If it doesn't, return the dto as is, else add the additional data
        if (ecosystem == null) return ecosystemDto;
        ecosystemDto.DisplayName = ecosystem.DisplayName;
        ecosystemDto.NumberOfStars = ecosystem.NumberOfStars;
        ecosystemDto.Description = ecosystem.Description;

        return ecosystemDto;
    }

    public async Task<string> UpdateDescription(descriptionRequestDto dto)
    {
        //Update description for ecosystem with given description
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
}