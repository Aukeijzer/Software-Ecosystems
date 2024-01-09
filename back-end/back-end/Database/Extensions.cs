namespace SECODashBackend.Database;

public static class Extensions
{
   public static void CreateDbIfNotExists(this IHost host)
   {
      using (var scope = host.Services.CreateScope())
      {
         var services = scope.ServiceProvider;
         //Create the ecosystems database if it does not exist, and initialise it.
         var ecosystemsContext = services.GetRequiredService<EcosystemsContext>();
         ecosystemsContext.Database.EnsureCreated();
         DbInitializer.Initialize(ecosystemsContext);
         //Create the users database if it does not exist, and initialise it.
         var usersContext = services.GetRequiredService<UserContext>();
         usersContext.Database.EnsureCreated();
         UserDbInitializer.Initialize(usersContext);
      }
   }
}