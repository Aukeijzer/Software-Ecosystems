namespace SECODashBackend.Services.Projects;

public interface IProjectsService
{
    public Task MineByTopicAsync(string topic);
}