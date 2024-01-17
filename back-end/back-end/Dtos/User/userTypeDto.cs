using System.Runtime.Serialization;

namespace SECODashBackend.Dtos.User;

/// <summary>
/// Represents a data transfer object for user type.
/// </summary>
public class UserTypeDto
{
    [DataMember(Name = "userType")]
    public required Models.User.UserType UserType { get; init; }
}