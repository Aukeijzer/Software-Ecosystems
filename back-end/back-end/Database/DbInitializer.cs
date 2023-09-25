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
         Projects = new List<Project>
         {
            new()
            {
               Name = "awesome-agriculture", About = "Open source technology for agriculture, farming, and gardening",
               Owner = "brycejohnston", NumberOfStars = 1100,
               ReadMe = "A curated list of awesome open source technology for agriculture, farming, and gardening."
            },
         }
      });
      context.SaveChanges();
   }
}