using SECODashBackend.Models;

namespace SECODashBackend.Services;

public interface IEcosystemsService
{
   public List<Ecosystem> GetAll();
}