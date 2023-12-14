using Microsoft.EntityFrameworkCore;
using SECODashBackend.Database;
using SECODashBackend.Models;

namespace SECODashBackend.Services.Users;

public class UsersService(UserContext userContext)
{
    public async Task<List<User>> GetAllAsync()
    {
        var users = await userContext.Users
            .AsNoTracking()
            .ToListAsync();
        return users;
    }

    public async Task AddUserAsync(string[] user)
    {
        userContext.Users.Add(new User
        {
            Id = user[0],
            UserName = user[1],
            UserType = user[2]
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
        User user = new User{Id = id,UserName = "",UserType =""};
        userContext.Remove(user);
        await userContext.SaveChangesAsync();
    }
}