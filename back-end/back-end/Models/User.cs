﻿using Microsoft.EntityFrameworkCore;

namespace SECODashBackend.Models;
/// <summary>
/// A class that represents a User
/// </summary>
[Index(nameof(UserName),IsUnique = true)]
public class User
{
    //Id is a string of numbers generated by the login provider.
    public required string Id { get; set; }
    //Username is provided by the user on creation of an account.
    public required string UserName { get; set; }
    //Usertype is the permissions level of the User.
    public required string UserType { get; set; }
}