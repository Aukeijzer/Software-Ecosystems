using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.DataConverters;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Models;
using SECODashBackend.Services.DataProcessor;
using SECODashBackend.Services.ProgrammingLanguages;
using SECODashBackend.Services.Projects;
using SECODashBackend.Services.Spider;

namespace SECODashBackend.Services.Ecosystems;
    
public class EcosystemsService : IEcosystemsService
{
    private readonly EcosystemsContext _dbContext;
    private readonly IDataProcessorService _dataProcessorService;
    private readonly IProjectsService _projectsService;

    public EcosystemsService(
        EcosystemsContext dbContext,
        IProjectsService projectsService,
        IDataProcessorService dataProcessorService)
    {
        _dbContext = dbContext;
        _projectsService = projectsService;
        _dataProcessorService = dataProcessorService;
    }
    public async Task<List<EcosystemDto>> GetAllAsync()
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

    public async Task<EcosystemDto> GetByIdAsync(string id)
    {
        var ecosystem = await _dbContext.Ecosystems
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Id == id);
        
        if (ecosystem == null) throw new KeyNotFoundException();
        
        return EcosystemConverter.ToDto(ecosystem);
    }

    public async Task<EcosystemDto> GetByNameAsync(string name)
    {
        var ecosystem = await _dbContext.Ecosystems
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.Name == name);
        
        if (ecosystem == null) throw new KeyNotFoundException();

        var ecosystemDto = EcosystemConverter.ToDto(ecosystem);
        
        // Retrieve the projects belonging to the ecosystem
        var projects = await _projectsService.GetByTopicAsync(ecosystem.Name);
        var projectList = projects.ToList();
        
        // Get the most popular programming languages associated with the ecosystem
        var topLanguages = TopProgrammingLanguagesService.GetTopLanguagesForEcosystem(projectList);
        
        // Add the projects and languages to the ecosystem dto
        ecosystemDto.Projects = projectList.Select(ProjectConverter.ToProjectDto);
        ecosystemDto.TopLanguages = topLanguages;
        
        return ecosystemDto;
    }
}