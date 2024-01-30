using SECODashBackend.Models;

namespace SECODashBackend.Database;
/// <summary>
/// Initializes the Users database with the initial RootAdmin User.
/// </summary>
public static class UserDbInitializer
{
    public static void Initialize(EcosystemsContext context)
    {
        var user = context.Users.FirstOrDefault();
        if (user != null) return;
        
        context.Users.Add(new User
        {
            Id = "152970440",
            UserName = "4087568626",
            Type = User.UserType.RootAdmin,
        });
        context.SaveChanges();
    }
}