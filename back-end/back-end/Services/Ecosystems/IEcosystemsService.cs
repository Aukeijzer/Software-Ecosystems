using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Ecosystems;

public interface IEcosystemsService
{
   public Task<List<EcosystemDto>> GetAllAsync();
   public Task<int> AddAsync(Ecosystem ecosystem);
   Task<EcosystemDto> GetByIdAsync(string id);
   Task<EcosystemDto> GetByNameAsync(string name);
}