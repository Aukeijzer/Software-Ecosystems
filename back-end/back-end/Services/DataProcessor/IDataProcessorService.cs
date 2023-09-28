using SECODashBackend.Dtos;

namespace SECODashBackend.Services.DataProcessor;
public interface IDataProcessorService
{
    public Task<IEnumerable<ProjectTopicsDto>> GetTopics(IEnumerable<ReadmeDto> readmeDtos);
}