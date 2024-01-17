namespace SECODashBackend.Dtos.User;
using System.Runtime.Serialization;

/// <summary>
/// Represents a request DTO for user type.
/// </summary>
public class UserTypeRequestDto
{
    [DataMember(Name = "id")] 
    public required string Id { get; init; }
    
    [DataMember(Name = "userName")] 
    public required string Username { get; init; }
}