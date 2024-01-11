﻿using System.Runtime.Serialization;
using System.Runtime.Serialization.DataContracts;
using Microsoft.EntityFrameworkCore;

namespace SECODashBackend.Models;
/// <summary>
/// A class that represents a User that has an <see cref="Id"/>, a <see cref="UserName"/> and a <see cref="UserType"/>.
/// </summary>
[Index(nameof(UserName),IsUnique = true)]
public class User
{
    /// <summary>
    /// Id is a string of numbers generated by the login provider.
    /// </summary>
    [DataMember(Name = "id")]
    public required string Id { get; set; }
    /// <summary>
    /// Username is provided by the user on creation of an account.
    /// </summary>
    [DataMember(Name = "userName")]
    public required string UserName { get; set; }
    /// <summary>
    /// Usertype is the permissions level of the User.
    /// </summary>
    [DataMember(Name = "userType")]
    public required string UserType { get; set; }
    
    [DataMember(Name = "ecosystems")] 
    public List<Ecosystem> Ecosystems { get; set; } = [];
}