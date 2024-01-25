using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;

namespace SECODashBackend.Dtos.Ecosystem;
/// <summary>
/// This class represents a data transfer object of all data needed to create a new ecosystem.
/// </summary>
public class EcosystemCreationDto
{
    [DataMember(Name = "ecosystemName")]
    public string EcosystemName { get; set; }
    
    [DataMember(Name = "description")]
    public string Description { get; set; }
    [DataMember(Name = "email")]
    public string Email { get; set; }
    
    [DataMember(Name = "topics")]
    public List<string> Topics { get; set; }
    
    [DataMember(Name = "technologies")]
    public List<string> Technologies { get; set; }
    
    [DataMember(Name = "excluded")]
    public List<string> Excluded { get; set; }
 }