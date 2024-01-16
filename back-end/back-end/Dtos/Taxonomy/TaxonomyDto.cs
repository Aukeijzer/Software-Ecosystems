using System.Runtime.Serialization;
using SECODashBackend.Dtos.Ecosystem;

namespace SECODashBackend.Dtos.Taxonomy;

public class TaxonomyDto
{
    [DataMember(Name = "term")] 
    public required string Term { get; set; }

    [DataMember(Name = "ecosystems")]
    public List<EcosystemDto> Ecosystems { get; init; }
    
    [DataMember(Name = "technologies")]
    public List<TechnologyDto> Technologies { get; init; }
}