namespace SECODashBackend.Services.Projects;

public interface IProjectsService
{
    public Task MineByTopicAsync(string topic, int amount);
    public Task MineByKeywordAsync(string topic, int amount);
}