using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.DataConverters;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Models;
using SECODashBackend.Services.Analysis;

namespace SECODashBackend.Services.Ecosystems;
    
public class EcosystemsService : IEcosystemsService
{
    private const int DefaultNumberOfTopItems = 10;
    private readonly EcosystemsContext _dbContext;
    private readonly IAnalysisService _analysisService;

    public EcosystemsService(
        EcosystemsContext dbContext,
        IAnalysisService analysisService)
    {
        _dbContext = dbContext;
        _analysisService = analysisService;
    }
    public async Task<List<EcosystemOverviewDto>> GetAllAsync()
    {
        var ecosystems = await _dbContext.Ecosystems
            .AsNoTracking()
            .ToListAsync();
        return ecosystems.Select(EcosystemConverter.ToDto).ToList();
    }

    // TODO: convert to accept a dto instead of an Ecosystem
    public async Task<int> AddAsync(Ecosystem ecosystem)
    {
        await _dbContext.Ecosystems.AddAsync(ecosystem);
        return await _dbContext.SaveChangesAsync();
    }

    private async Task<Ecosystem?> GetByNameAsync(string name)
    {
        return await _dbContext.Ecosystems
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Name == name);
    }

    public async Task<EcosystemDto> GetByTopicsAsync(EcosystemRequestDto dto)
    {
        if (dto.Topics.Count == 0) throw new ArgumentException("Number of topics cannot be 0");

        var ecosystemDto = await _analysisService.AnalyzeEcosystemAsync(
            dto.Topics,
            dto.NumberOfTopLanguages ?? DefaultNumberOfTopItems,
            dto.NumberOfTopSubEcosystems ?? DefaultNumberOfTopItems);

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
}