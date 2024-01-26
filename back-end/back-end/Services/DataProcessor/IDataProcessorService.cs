using SECODashBackend.Dtos.Topic;
using SECODashBackend.Dtos.Project;

namespace SECODashBackend.Services.DataProcessor;

/// <summary>
/// Interface for the service is responsible for sending the readme data to the data processor and returning the resulting topics.
/// </summary>
public interface IDataProcessorService
{
    /// <summary>
    /// Sends the readme data to the data processor and returns the resulting topics.
    /// </summary>
    /// <param name="readmeDtos">The readme data to be sent to the data processor.</param>
    /// <returns>The resulting topics.</returns>
    public Task<IEnumerable<TopicResponseDto>> GetTopics(IEnumerable<ProjectDto> projectDtos);
    /// <summary>
    /// Adds topics to the given projectDtos
    /// </summary>
    /// <param name="topicDtos"></param>
    /// <param name="projectDtos"></param>
    public void AddTopicsToProjects(IEnumerable<TopicResponseDto> topicDtos, IEnumerable<ProjectDto> projectDtos);

}