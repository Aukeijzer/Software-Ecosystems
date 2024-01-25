   using RestSharp;
using SECODashBackend.Dtos.Topic;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.DataProcessor;

/// <summary>
/// This service is responsible for sending the readme data to the data processor and returning the resulting topics.
/// </summary>
public class DataProcessorService : IDataProcessorService
{
   private readonly RestClient _client;
   
   public DataProcessorService(string connectionString)
   {
      var options = new RestClientOptions(connectionString);
      options.MaxTimeout = 100000000;
      _client = new RestClient(options);
   }
   
   /// <summary>
   /// Sends the readme data to the data processor and returns the resulting topics.
   /// </summary>
   /// <param name="readmeDtos">The readme data.</param>
   /// <returns>The resulting topics.</returns>
   /// <exception cref="HttpRequestException">Thrown if the request cannot be deserialized into a List of ProjectTopicsDtos.</exception>
   public async Task<IEnumerable<TopicResponseDto>> GetTopics(IEnumerable<ProjectDto> projectDtos)
   {
      var readmeDtos = ConvertToTopicDto(projectDtos);
      var request = new RestRequest("extract-topics", Method.Post).AddJsonBody(readmeDtos);
      
      return await _client.PostAsync<List<TopicResponseDto>>(request) ?? throw new HttpRequestException();
   }
   
   /// <summary>
   /// Adds topics from topics extracted from data processor to the projects
   /// </summary>
   /// <param name="topicDtos"></param>
   /// <param name="projectDtos"></param>
   public void AddTopicsToProjects(IEnumerable<TopicResponseDto> topicDtos, IEnumerable<ProjectDto> projectDtos)
   {
      foreach (var dto in projectDtos) 
      {
         var topicDto = topicDtos.FirstOrDefault(t => t.ProjectId == dto.Id);
         if (topicDto != null)
         {
            dto.AdditionalTopics = topicDto.Topics;
         }
      }
   }
   
   /// <summary>
   /// Converts a list projectDto to a list of topicRequestDtos
   /// </summary>
   /// <param name="projectDtos"></param>
   /// <returns> List of TopicRquestDtos </returns>
   private IEnumerable<TopicRequestDto> ConvertToTopicDto(IEnumerable<ProjectDto> projectDtos)
   {
      var readmeDtos = projectDtos.Select(dto => new TopicRequestDto
      {
         Id = dto.Id, 
         Name = dto.Name, 
         Description = dto.Description, 
         Readme = dto.ReadMe
      }).ToList();

      return readmeDtos;
   }
}
