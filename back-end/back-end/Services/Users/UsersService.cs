using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Users;
/// <summary>
/// This service is responsible for handling all user-related requests.
/// </summary>
/// <param name="userContext">Is used to interact with the database.</param>
public class UsersService(UserContext userContext)
{
    /// <summary>
    /// Retrieve all users from the database.
    /// </summary>
    /// <returns></returns>
    public async Task<List<User>> GetAllAsync()
    {
        var users = await userContext.Users
            .AsNoTracking()
            .ToListAsync();
        return users;
    }
    
    /// <summary>
    /// Add a user to the database using a given id and username.
    /// All new users start as User.
    /// </summary>
    public async Task AddUserAsync(string id, string userName)
    {
        userContext.Users.Add(new User
        {
            Id = id,
            UserName = userName,
            UserType = "User"
        });
        await userContext.SaveChangesAsync();
    }
    /// <summary>
    /// This method returns the <see cref="User"/> with the provided UserName.
    /// </summary>
    public async Task<User?> GetByNameAsync(string userName)
    {
        return await userContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.UserName == userName);
    }
    /// <summary>
    /// This method removes the <see cref="User"/> with the provided Id from the database.
    /// </summary>
    public async Task RemoveById(string id)
    {
        var user = new User{Id = id,UserName = "",UserType =""};
        userContext.Remove(user);
        await userContext.SaveChangesAsync();
    }
    /// <summary>
    /// This method handles the Login requests received.
    /// If the User exists, return the UserType. Otherwise create a new User and then return the UserType.
    /// </summary>
    public async Task<string> LoginRequest(string id, string userName)
    {
        //Check if the User already exists.
        var user = await userContext.Users.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
        //Return the UserType,
        if (user != null) return user.UserType;
        //Or create a new User from the Id and UserName, and return the default value.
        await AddUserAsync(id, userName);
        return "User";

    }

  
    
    
}