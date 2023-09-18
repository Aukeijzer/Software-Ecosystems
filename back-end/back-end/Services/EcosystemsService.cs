using SECODashBackend.Models;

namespace SECODashBackend.Services;
    
public class EcosystemsService : IEcosystemsService
{
    public List<Ecosystem> GetAll()
    {
        List<Ecosystem> sampleEcosystems = new List<Ecosystem>
        { new (
            1,
            "agriculture", 
            "Agriculture",
            new List<Project>
                {
                    new(
                    "awesome-agriculture", 
                    1, 
                    "Open source technology for agriculture, farming, and gardening", 
                    null,
                    null,
                    1100)},
    34534)
        };
        return sampleEcosystems;
    }
}