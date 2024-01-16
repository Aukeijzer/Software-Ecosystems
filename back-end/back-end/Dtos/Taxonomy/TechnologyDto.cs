using System.Runtime.Serialization;
using SECODashBackend.Dtos.Ecosystem;

namespace SECODashBackend.Dtos.Taxonomy;

public class TechnologyDto
{
    [DataMember(Name = "term")]
    public required string Term { get; set; }

    [DataMember(Name = "ecosystems")]
    public List<EcosystemDto> Ecosystems { get; init; }
}