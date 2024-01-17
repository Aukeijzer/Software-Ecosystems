using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Ecosystems;

/// <summary>
/// Interface for the service that is responsible for handling all ecosystem-related requests.
/// </summary>
public interface IEcosystemsService
{
   /// <summary>
   /// Returns all top-level ecosystems.
   /// </summary>
   /// <returns>All top-level ecosystems.</returns>
   public Task<List<EcosystemOverviewDto>> GetAllAsync();
   /// <summary>
   /// Returns the ecosystem that has the given topics.
   /// </summary>
   /// <param name="dto">The Dto that contains the request information of the ecosystem to get.</param>
   /// <returns>The ecosystem defined by the given topics.</returns>
   public Task<EcosystemDto> GetByTopicsAsync(EcosystemRequestDto dto);
}