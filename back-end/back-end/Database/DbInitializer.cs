using SECODashBackend.Models;

namespace SECODashBackend.Database;

public static class DbInitializer
{
   public static void Initialize(EcosystemsContext context)
   {
      var ecosystem = context.Ecosystems.FirstOrDefault();
      if (ecosystem != null) return;

      context.Ecosystems.Add(new Ecosystem
      {
         Name = "agriculture",
         DisplayName = "Agriculture",
         Description = "Software related to agriculture",
         NumberOfStars = 34565,
         Projects = new List<Project>(),
      });
      context.Ecosystems.Add(new Ecosystem
      {
         Name = "quantum",
         DisplayName = "Quantum",
         Description = "Software related to quantum mechanics",
         NumberOfStars = 4352,
         Projects = new List<Project>(),
      });
      context.SaveChanges();
   }
}