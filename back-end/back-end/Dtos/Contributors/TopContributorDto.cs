using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.Contributors;

public class TopContributorDto
{
    [DataMember(Name = "login")]
    public required string Login { get; init; }
    [DataMember(Name = "contributions")]
    public int Contributions { get; init; }
}