using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.DataConverters;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Models;
using SECODashBackend.Services.DataProcessor;
using SECODashBackend.Services.ElasticSearch;
using SECODashBackend.Services.Projects;

namespace SECODashBackend.Services.Ecosystems;
    
public class EcosystemsService : IEcosystemsService
{
    private const int DefaultNumberOfTopItems = 10;
    private readonly EcosystemsContext _dbContext;
    private readonly IDataProcessorService _dataProcessorService;
    private readonly IProjectsService _projectsService;
    private readonly IElasticsearchService _elasticsearchService;

    public EcosystemsService(
        EcosystemsContext dbContext,
        IProjectsService projectsService,
        IDataProcessorService dataProcessorService,
        IElasticsearchService elasticsearchService)
    {
        _dbContext = dbContext;
        _projectsService = projectsService;
        _dataProcessorService = dataProcessorService;
        _elasticsearchService = elasticsearchService;
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
        
        var ecosystemDto = await _elasticsearchService.GetEcosystemData(
            dto.Topics,
            dto.NumberOfTopLanguages ?? DefaultNumberOfTopItems,
            dto.NumberOfTopSubEcosystems ?? DefaultNumberOfTopItems);

        if (dto.Topics.Count != 1) return ecosystemDto;
            
        var ecosystem = await GetByNameAsync(dto.Topics.First());
            
        if (ecosystem != null)
        {
            ecosystemDto.DisplayName = ecosystem.DisplayName;
            ecosystemDto.NumberOfStars = ecosystem.NumberOfStars;
            ecosystemDto.Description = ecosystem.Description;
        }
        return ecosystemDto;
    }
}