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
    private const int DefaultNumberOfDaysPerBucket = 30;

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
        
        var technologies = await GetTechnologyTaxonomy(dto.Topics.First());
        
        var ecosystemDto = await analysisService.AnalyzeEcosystemAsync(
            dto.Topics,
            technologies,
            dto.NumberOfTopLanguages ?? DefaultNumberOfTopItems,
            dto.NumberOfTopSubEcosystems ?? DefaultNumberOfTopItems,
            dto.NumberOfTopContributors ?? DefaultNumberOfTopItems,
            dto.NumberOfTopTechnologies ?? DefaultNumberOfTopItems,
            dto.NumberOfTopProjects ?? DefaultNumberOfTopItems,
            dto.StartTime,
            dto.EndTime,
            dto.NumbersOfDaysPerBucket ?? DefaultNumberOfDaysPerBucket);

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
    public async Task<bool> CreateEcosystem(EcosystemCreationDto dto)
    {
        //Check if the ecosystem already exists.
        var newEcosystem = dbContext.Ecosystems.AsNoTracking().Include(ecosystem => ecosystem.Users).FirstOrDefault(e => e.Name == dto.EcosystemName);
        if (newEcosystem != null)
        {
            return false;
        } 
        //Create a base for the new ecosystem.
        newEcosystem = new Ecosystem
        {
            Id = Guid.NewGuid().ToString(),
            Name = dto.EcosystemName,
            DisplayName = dto.EcosystemName,
            Description = dto.Description,
        };
        //Add the admin to the ecosystem.
        var admin = await dbContext.Users.SingleOrDefaultAsync(e => e.UserName == dto.Email);
        newEcosystem.Users.Add(admin);
        //Upload the changes to the database.
        dbContext.Ecosystems.Add(newEcosystem);
        await dbContext.SaveChangesAsync();
        return true;
    }
    /// <summary>
    /// Update the database with new topics and link the ecosystem to all its topics.
    /// </summary>
    /// <param name="dto">The data transfer object containing all required information.</param>
    /// <returns>Return a status message.</returns>
    public async Task<string> UpdateTopics(EcosystemCreationDto dto)
    {
        //Parse all topics provided by the dto and add new topics to the database.
        var taxonomy = ParseTopics(dto.Topics);
        var technologies = ParseTechnologies(dto.Technologies);
        var bannedTopics = ParseExcluded(dto.Excluded);
        //Add topics to the ecosystem.
        await AddTopicToEcosystem(taxonomy, technologies, bannedTopics, dto.EcosystemName);
        return "Successfully updated topics.";
    }
    /// <summary>
    /// Link all topics provided to the new ecosystem.
    /// </summary>
    /// <param name="tax"><see cref="List{T}"/> of <see cref="Taxonomy"/> to add to the ecosystem.</param>
    /// <param name="tech"><see cref="List{T}"/> of <see cref="Technology"/> to add to the ecosystem.</param>
    /// <param name="banned"><see cref="List{T}"/> of <see cref="BannedTopic"/> to add to the ecosystem.</param>
    /// <param name="eco">The name of the ecosystem to add topics to.</param>
    public async Task AddTopicToEcosystem(List<Taxonomy> tax, List<Technology> tech, List<BannedTopic> banned, string eco)
    {
        var ecosystem = dbContext.Ecosystems.FirstOrDefault(e => e.Name == eco);
        foreach (var term in tax)
        {
            var taxonomy = dbContext.Taxonomy.FirstOrDefault(t => t.Term == term.Term);
            ecosystem.Taxonomy.Add(taxonomy);
        }

        foreach (var term in tech)
        {
            var technology = dbContext.Technologies.FirstOrDefault(t => t.Term == term.Term);
            ecosystem.Technologies.Add(technology);
        }
        foreach (var term in banned)
        {
            var bannedTopic = dbContext.BannedTopics.FirstOrDefault(t => t.Term == term.Term);
            ecosystem.BannedTopics.Add(bannedTopic);
        }
        dbContext.Ecosystems.Update(ecosystem);
        await dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Parse provided <see cref="string"/> information into <see cref="Taxonomy"/> objects.
    /// </summary>
    /// <param name="topics"> <see cref="List{T}"/> of <see cref="string"/> to parse.</param>
    /// <returns>Return a <see cref="List{T}"/> of <see cref="Taxonomy"/>.</returns>
    private List<Taxonomy> ParseTopics(List<string> topics)
    {
        var newTaxonomy = new List<Taxonomy>();
        foreach (var topic in topics)
        {
            var taxonomy = dbContext.Taxonomy.FirstOrDefault(t => t.Term == topic);
            if (taxonomy == null)
            {
                dbContext.Taxonomy.Add(new Taxonomy { Term = topic });
            }
            newTaxonomy.Add(new Taxonomy{Term = topic});
        }
        dbContext.SaveChanges();
        return newTaxonomy;
    }
    /// <summary>
    /// Parse provided <see cref="string"/> information into <see cref="Technology"/> objects.
    /// </summary>
    /// <param name="technologies"> <see cref="List{T}"/> of <see cref="string"/> to parse.</param>
    /// <returns>Return a <see cref="List{T}"/> of <see cref="Technology"/>.</returns>
    private List<Technology> ParseTechnologies(List<string> topics)
    {
        var techModels = new List<Technology>();
        foreach (var topic in topics)
        {
            var technologies = dbContext.Technologies.FirstOrDefault(t => t.Term == topic);
            if (technologies == null)
            {
                dbContext.Technologies.Add(new Technology { Term = topic });
            }
            techModels.Add(new Technology{Term = topic});
        }

        dbContext.SaveChanges();
        return techModels;
    }

    /// <summary>
    /// Parse provided <see cref="string"/> information into <see cref="BannedTopic"/> objects.
    /// </summary>
    /// <param name="excluded"> <see cref="List{T}"/> of <see cref="string"/> to parse.</param>
    /// <returns>Return a <see cref="List{T}"/> of <see cref="BannedTopic"/>.</returns>
    private List<BannedTopic> ParseExcluded(List<string> topics)
    {
        var bannedTopics = new List<BannedTopic>();
        foreach (var topic in topics)
        {
            var technologies = dbContext.BannedTopics.FirstOrDefault(t => t.Term == topic);
            if (technologies == null)
            {
                dbContext.BannedTopics.Add(new BannedTopic { Term = topic });
            }
            bannedTopics.Add(new BannedTopic { Term = topic });
        }
        dbContext.SaveChanges();
        return bannedTopics;
    }
    /// <summary>
    /// Remove an existing top-level ecosystem from the database.
    /// </summary>
    /// <param name="ecosystem"></param>
    /// <returns></returns>
    public async Task<string> RemoveEcosystem(string ecosystem)
    {
        var deleted = dbContext.Ecosystems.FirstOrDefault(e => e.Name == ecosystem);
        if (deleted == null)
        {
            return "No such ecosystem";
        }
        dbContext.Ecosystems.Remove(deleted);
        await dbContext.SaveChangesAsync();
        return "Ecosystem has been deleted";
    }
    public async Task<List<Technology>> GetTechnologyTaxonomy(string ecosystemName)
    {
        var ecosystem = await GetByNameAsync(ecosystemName);
        if (ecosystem == null) throw new ArgumentException("Ecosystem not found");
        return ecosystem.Technologies;
    }
}