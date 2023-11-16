using Elastic.Clients.Elasticsearch;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Dtos.Project;


namespace SECODashBackend.Services.ElasticSearch;

public interface IElasticsearchService
{
   public Task AddProjects(IEnumerable<ProjectDto> projectDtos);
   public Task<List<ProjectDto>> GetProjectsByTopic(List<string> topics);
   public Task<EcosystemDto> GetEcosystemData(List<string> topics, int numberOfTopLanguages, int numberOfTopSubEcosystems);
   public Task<List<ProjectDto>> GetProjectsByDate(DateMath timeFrameMin, DateMath timeFrameMax);
}