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

        var ecosystemDto = await analysisService.AnalyzeEcosystemAsync(
            dto.Topics,
            dto.Technologies,
            dto.NumberOfTopLanguages ?? DefaultNumberOfTopItems,
            dto.NumberOfTopSubEcosystems ?? DefaultNumberOfTopItems,
            dto.NumberOfTopContributors ?? DefaultNumberOfTopItems,
            dto.NumberOfTopTechnologies ?? DefaultNumberOfTopItems,
            dto.NumberOfTopProjects ?? DefaultNumberOfTopItems);

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

    /// <summary>
    /// Create an ecosystem and add it to the Database for top-level ecosystems.
    /// </summary>
    /// <param name="dto">The data transfer object containing all required information.</param>
    public async Task CreateEcosystem(EcosystemCreationDto dto)
    {
        var newEcosystem = dbContext.Ecosystems.Include(ecosystem => ecosystem.Users).FirstOrDefault(e => e.Name == dto.EcosystemName);
        if(newEcosystem == null)
        {
            newEcosystem = new Ecosystem
            {
                Id = Guid.NewGuid().ToString(),
                Name = dto.EcosystemName,
                DisplayName = dto.EcosystemName,
            };
        }
        newEcosystem.Description = dto.Description;
        var admin = await dbContext.Users.SingleOrDefaultAsync(e => e.UserName == dto.Email);
        if (!newEcosystem.Users.Contains(admin))
        {
            newEcosystem.Users.Add(admin);
        }
        newEcosystem.Taxonomy = ParseTopics(dto.Topics);
        newEcosystem.Technologies = ParseTechnologies(dto.Technologies);
        newEcosystem.BannedTopics = ParseExcluded(dto.Excluded);
        dbContext.Ecosystems.Update(newEcosystem);
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Parse provided <see cref="string"/> information into <see cref="Taxonomy"/> objects.
    /// </summary>
    /// <param name="topics"> <see cref="List{T}"/> of <see cref="string"/> to parse.</param>
    /// <returns>Return a <see cref="List{T}"/> of <see cref="Taxonomy"/>.</returns>
    private static List<Taxonomy> ParseTopics(List<string> topics)
    {
        var taxonomy = new List<Taxonomy>();
        foreach (var topic in topics)
        {
            taxonomy.Add(new Taxonomy{Term = topic});
        }
        return taxonomy;
    }
    /// <summary>
    /// Parse provided <see cref="string"/> information into <see cref="Technology"/> objects.
    /// </summary>
    /// <param name="technologies"> <see cref="List{T}"/> of <see cref="string"/> to parse.</param>
    /// <returns>Return a <see cref="List{T}"/> of <see cref="Technology"/>.</returns>
    private static List<Technology> ParseTechnologies(List<string> technologies)
    {
        var techModels = new List<Technology>();
        foreach (var technology in technologies)
        {
            techModels.Add(new Technology{Term = technology});
        }
        return techModels;
    }

    /// <summary>
    /// Parse provided <see cref="string"/> information into <see cref="BannedTopic"/> objects.
    /// </summary>
    /// <param name="excluded"> <see cref="List{T}"/> of <see cref="string"/> to parse.</param>
    /// <returns>Return a <see cref="List{T}"/> of <see cref="BannedTopic"/>.</returns>
    private static List<BannedTopic> ParseExcluded(List<string> excluded)
    {
        var bannedTopics = new List<BannedTopic>();
        foreach (var exclusion in excluded)
        {
            bannedTopics.Add(new BannedTopic{Term = exclusion});
        }
        return bannedTopics;
    }
}