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
    //Retrieve all users from the database.
    public async Task<List<User>> GetAllAsync()
    {
        var users = await userContext.Users
            .AsNoTracking()
            .ToListAsync();
        return users;
    }
    //Add a user to the database using a given id and username.
    //All new users start as User.
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

    public async Task<User?> GetByNameAsync(string userName)
    {
        return await userContext.Users
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.UserName == userName);
    }

    public async Task RemoveById(string id)
    {
        var user = new User{Id = id,UserName = "",UserType =""};
        userContext.Remove(user);
        await userContext.SaveChangesAsync();
    }

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