using SECODashBackend.Models;

namespace SECODashBackend.Services.Ecosystems;

public interface IEcosystemsService
{
   public List<Ecosystem> GetAll();
   void Add(Ecosystem ecosystem);
   Ecosystem? GetById(long id);
   Ecosystem? GetByName(string name);
   
}