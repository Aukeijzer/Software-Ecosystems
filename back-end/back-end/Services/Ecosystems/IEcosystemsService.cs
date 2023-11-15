using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Ecosystems;

public interface IEcosystemsService
{
   public Task<List<EcosystemOverviewDto>> GetAllAsync();
   public Task<int> AddAsync(Ecosystem ecosystem);
   public Task<EcosystemDto> GetByTopicsAsync(EcosystemRequestDto dto);
}