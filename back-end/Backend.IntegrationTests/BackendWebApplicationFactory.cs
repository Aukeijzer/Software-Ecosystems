using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SECODashBackend.Models;

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
                "Develop:d2VzdGV1cm9wZS5henVyZS5lbGFzdGljLWNsb3VkLmNvbSQwMmU3OTE1NjYwZGE0MDMwOTk1YTU5MWI0ZWM0ODBiOCRmZjI2OTkyMjhhNmY0YzQzYTQ5Mzk0NTJjNzVhZmE4MQ==",
                new ApiKey("V1RYTlE0d0J1WDU3Ty1QRDlIYW86dTBLdXUtdjFRT2lvd2x3dVQwOXlzUQ=="))
            .DefaultMappingFor<Project>(i => i
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