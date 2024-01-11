using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Ecosystems;

/// <summary>
/// Interface for the service that is responsible for handling all ecosystem-related requests.
/// </summary>
public interface IEcosystemsService
{
   public Task<List<EcosystemOverviewDto>> GetAllAsync();
   public Task<EcosystemDto> GetByTopicsAsync(EcosystemRequestDto dto);
}