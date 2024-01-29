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
            Id = "9864834",
            UserName = "3898088433",
            Type = User.UserType.RootAdmin,
        });
        context.SaveChanges();
    }
}