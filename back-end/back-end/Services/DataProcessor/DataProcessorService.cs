using RestSharp;
using SECODashBackend.Dtos;

namespace SECODashBackend.Services.DataProcessor;

public class DataProcessorService : IDataProcessorService
{
   private readonly RestClient _client = new("http://localhost:5000");

   public async Task<IEnumerable<ProjectTopicsDto>?> GetTopics(IEnumerable<ReadmeDto> readmeDtos)
   {
      var request = new RestRequest("extract-topics", Method.Post).AddJsonBody(readmeDtos);
      
      // Throw an exception if the request cannot be deserialized into a List of ProjectTopicsDtos
      return  await _client.PostAsync<List<ProjectTopicsDto>>(request) ?? throw new HttpRequestException();
   }
}
