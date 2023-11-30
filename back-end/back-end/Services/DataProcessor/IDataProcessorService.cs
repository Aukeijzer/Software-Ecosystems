using SECODashBackend.Dtos.Topic;

namespace SECODashBackend.Services.DataProcessor;

/// <summary>
/// Interface for the service is responsible for sending the readme data to the data processor and returning the resulting topics.
/// </summary>
public interface IDataProcessorService
{
    public Task<IEnumerable<TopicResponseDto>> GetTopics(IEnumerable<TopicRequestDto> readmeDtos);
}