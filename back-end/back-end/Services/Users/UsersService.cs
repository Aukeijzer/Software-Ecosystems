using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Users;
/// <summary>
/// This service is responsible for handling all user-related requests.
/// </summary>
/// <param name="userContext">Is used to interact with the database.</param>
public class UsersService(EcosystemsContext userContext)
{
    /// <summary>
    /// Retrieve all users from the database.
    /// </summary>
    /// <returns><see cref="List{T}"/> of type <see cref="User"/>.</returns>
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
    /// <param name="id">The Id as <see cref="string"/> generated by the login provider. </param>
    /// <param name="userName">The UserName as <see cref="string"/> provided by the user. </param>
    public async Task AddUserAsync(string id, string userName)
    {
        userContext.Users.Add(new User
        {
            Id = id,
            UserName = userName,
            Type = User.UserType.User
        });
        await userContext.SaveChangesAsync();
    }
    /// <summary>
    /// This method returns the <see cref="User"/> with the provided UserName.
    /// </summary>
    /// <param name="userName">The userName of the <see cref="User"/> to lookup in the database, provided as <see cref="string"/>. </param>
    /// <returns>Returns the <see cref="User"/> if it is present in the database. Returns null if not found.</returns>
    public async Task<User?> GetByNameAsync(string userName)
    {
        return await userContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.UserName == userName);
    }
    /// <summary>
    /// This method removes the <see cref="User"/> with the provided Id from the database.
    /// </summary>
    /// <param name="id">The Id of the <see cref="User"/> to remove from the database, provided as <see cref="string"/>.</param>
    public async Task RemoveById(string id)
    {
        var user = new User{Id = id,UserName = "",Type = User.UserType.User};
        userContext.Remove(user);
        await userContext.SaveChangesAsync();
    }
    /// <summary>
    /// This method handles the Login requests received.
    /// If the User exists, return the UserType. Otherwise create a new User and then return the UserType.
    /// </summary>
    /// <param name="id">The Id of the <see cref="User"/> that is trying to log in.</param>
    /// <param name="userName">The UserName of the <see cref="User"/> that is trying to log in.</param>
    /// <returns>Returns the <see cref="User.Type"/> of the <see cref="User"/> that is logging in.</returns>
    public async Task<User.UserType> LoginRequest(string id, string userName)
    {
        //Check if the User already exists.
        var user = await userContext.Users.AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
        //Return the UserType,
        if (user != null) return user.Type;
        //Or create a new User from the Id and UserName, and return the default value.
        await AddUserAsync(id, userName);
        return User.UserType.User;

    }
}