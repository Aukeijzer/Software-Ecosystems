using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using SECODashBackend.Dtos.Ecosystem;

namespace Backend.IntegrationTests;

public class EcosystemsTest(BackendWebApplicationFactory<Program> factory) : IClassFixture<BackendWebApplicationFactory<Program>>
{
    private readonly BackendWebApplicationFactory<Program> _factory = factory;
    private readonly HttpClient _client = factory.CreateClient(new WebApplicationFactoryClientOptions
    {
        AllowAutoRedirect = false
    });

    private readonly JsonSerializerOptions _serializerOptions = new JsonSerializerOptions
        { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

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
        // Arrange
        var requestDto = new EcosystemRequestDto
        {
            Topics = new List<string> { "topic1" },
            NumberOfTopLanguages = 2,
            NumberOfTopSubEcosystems = 3
        };
        
        // Act
        var response = await _client.PostAsJsonAsync("/ecosystems", requestDto);
        var stream = await response.Content.ReadAsStreamAsync();
        var s = await response.Content.ReadAsStringAsync();
        var ecosystems =
            await JsonSerializer.DeserializeAsync<EcosystemDto>(stream, _serializerOptions);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}