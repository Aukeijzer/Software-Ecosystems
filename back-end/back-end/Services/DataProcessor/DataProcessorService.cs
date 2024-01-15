using RestSharp;
using SECODashBackend.Dtos.Topic;

namespace SECODashBackend.Services.DataProcessor;

/// <summary>
/// This service is responsible for sending the readme data to the data processor and returning the resulting topics.
/// </summary>
public class DataProcessorService : IDataProcessorService
{
   private readonly RestClient _client = new("http://localhost:5000");

   /// <summary>
   /// Sends the readme data to the data processor and returns the resulting topics.
   /// </summary>
   /// <param name="readmeDtos">The readme data.</param>
   /// <returns>The resulting topics.</returns>
   /// <exception cref="HttpRequestException">Thrown if the request cannot be deserialized into a List of ProjectTopicsDtos.</exception>
   public async Task<IEnumerable<TopicResponseDto>> GetTopics(IEnumerable<TopicRequestDto> readmeDtos)
   {
      var request = new RestRequest("extract-topics", Method.Post).AddJsonBody(readmeDtos);
      
      // Throw an exception if the request cannot be deserialized into a List of ProjectTopicsDtos
      return await _client.PostAsync<List<TopicResponseDto>>(request) ?? throw new HttpRequestException();
   }
}
