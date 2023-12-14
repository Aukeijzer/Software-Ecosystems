namespace SECODashBackend.Dtos.users;
using System.Runtime.Serialization;

public class userTypeRequestDto
{
    [DataMember(Name = "id")] 
    public required string Id { get; init; }
    
    [DataMember(Name = "userName")] 
    public required string Username { get; init; }
}