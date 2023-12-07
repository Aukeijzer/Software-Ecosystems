using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Dtos.ProgrammingLanguage;

namespace Backend.IntegrationTests;

/// <summary>
/// Integration tests for the EcosystemsController.
/// Uses the BackendWebApplicationFactory that replaces the standard ElasticsearchClient with one that uses a dedicated test index.
/// See Backend.IntegrationTests/BackendWebApplicationFactory.cs for more information.
/// </summary>
public class EcosystemsTest(BackendWebApplicationFactory<Program> factory) : IClassFixture<BackendWebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        AllowAutoRedirect = false
    });

    private readonly JsonSerializerOptions _serializerOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

    [Fact]
    public async Task Get_Ecosystems_ReturnsCorrectEcosystems()
    {
        // Arrange
        var expectedNames = new List<string> { "Agriculture", "Artificial Intelligence", "Quantum" };
        
        // Act
        var response = await _client.GetAsync("/ecosystems");
        var stream = await response.Content.ReadAsStreamAsync();
        var ecosystems =
            await JsonSerializer.DeserializeAsync<List<EcosystemOverviewDto>>(stream, _serializerOptions);
        var ecosystemNames = ecosystems?.Select(e => e.DisplayName);
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        ecosystemNames.Should().BeEquivalentTo(expectedNames);
    }

    [Fact]
    public async Task Post_Ecosystems_ReturnsCorrectEcosystems()
    {
        const string topic1 = "topic1";
        const string topic2 = "topic2";
        const string topic3 = "topic3";
        const string topic4 = "topic4";
        
        // Arrange
        var requestDto = new EcosystemRequestDto
        {
            Topics = new List<string> { topic1 },
            NumberOfTopLanguages = 2,
            NumberOfTopSubEcosystems = 3
        };
        
        var expectedResponse = new EcosystemDto
        {
            Topics = new List<string> { topic1 },
            SubEcosystems = new List<SubEcosystemDto>
            {
                new()
                {
                    ProjectCount = 17,
                    Topic = topic2
                },
                new()
                {
                    ProjectCount = 3,
                    Topic = topic3
                },
                new()
                {
                    ProjectCount = 2,
                    Topic = topic4
                }
            },
            TopLanguages = new List<ProgrammingLanguageDto>
            {
               new()
               {
                   Language = "Java",
                   Percentage = 100
               } 
            }
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("/ecosystems", requestDto);
        var stream = await response.Content.ReadAsStreamAsync();
        var ecosystems =
            await JsonSerializer.DeserializeAsync<EcosystemDto>(stream, _serializerOptions);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        ecosystems.Should().BeEquivalentTo(expectedResponse);
    }
}