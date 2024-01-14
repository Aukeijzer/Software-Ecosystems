namespace SECODashBackend.Dtos.users;
using System.Runtime.Serialization;

/// <summary>
/// Represents a data transfer object for user type.
/// </summary>
public class UserTypeDto
{
    [DataMember(Name = "userType")]
    public required string UserType { get; init; }
}