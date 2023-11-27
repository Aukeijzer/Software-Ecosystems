using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using SECODashBackend.Models;

namespace Backend.IntegrationTests;

public class BackendWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        var settings = new ElasticsearchClientSettings(
                "Develop:d2VzdGV1cm9wZS5henVyZS5lbGFzdGljLWNsb3VkLmNvbTo0NDMkNWYwNTY0ZDQ2YWRjNDUxMGFiNmQyZTM5MjI3ZDQzOTIkYzRkYTgyYTg0NmQwNGMxYTlkZmUxYzg0MTkyNmY4N2U=",
                new ApiKey("ejZsWHpZc0JaeUkxNld5RVFDUzM6S21rT2tILW1UaEs3M0JGQWltdmJHUQ=="))
            .DefaultMappingFor<Project>(i => i
                .IndexName("projects-01")
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