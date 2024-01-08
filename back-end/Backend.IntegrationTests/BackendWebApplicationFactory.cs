using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SECODashBackend.Dtos.Project;

namespace Backend.IntegrationTests;
/// <summary>
/// This method is used to create a WebApplicationFactory for the integration tests.
/// </summary>
/// <typeparam name="TProgram"></typeparam>
public class BackendWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Replace the standard ElasticsearchClient by one that maps Project related requests to a dedicated test index
        var settings = new ElasticsearchClientSettings(
                "Develop:d2VzdGV1cm9wZS5henVyZS5lbGFzdGljLWNsb3VkLmNvbTo0NDMkYTQ2MDIwZTE1Yzc4NGM3YThmMzdlZDMwZjIzM2E3NmYkNjc4ZTU3ZDUzODFiNDhhNGJiNTJkOTA0ZDAxMTI2Y2U=",
                new ApiKey("MnA5MDZJd0JWTDlYTVJGS0lTam06YmV0RmUtd19SMTY4THNQWU4xeHdCdw=="))
            .DefaultMappingFor<ProjectDto>(i => i
                .IndexName("integration-test-01")
            );
        builder.ConfigureTestServices(services =>
        {
            var elasticClientDescriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(ElasticsearchClient));
            services.Remove(elasticClientDescriptor);

            services.AddSingleton(
                new ElasticsearchClient(settings));
        });
    }
}