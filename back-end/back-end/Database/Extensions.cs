using Microsoft.EntityFrameworkCore;
using SECODashBackend.Services.Ecosystems;
using SECODashBackend.Services.Scheduler;

namespace SECODashBackend.Database;

public static class Extensions
{
   /// <summary>
   /// This method checks if the Ecosystems database exists. If it does not exist, create it and fill it with initial values.
   /// </summary>
   public static void CreateDbIfNotExists(this IHost host)
   {
      using (var scope = host.Services.CreateScope())
      {
         var services = scope.ServiceProvider;
         var ecosystemsContext = services.GetRequiredService<EcosystemsContext>();
         ecosystemsContext.Database.EnsureCreated();
         var ecosystemService = services.GetRequiredService<IEcosystemsService>();
         UserDbInitializer.Initialize(ecosystemsContext);
         DbInitializer.Initialize(ecosystemsContext, ecosystemService);
      }
   }
   /// <summary>
   /// Schedule jobs for the ecosystems that exist on startup.
   /// </summary>
   public static void ScheduleInitialJobs(this IHost host)
   {
      using (var scope = host.Services.CreateScope())
      {
         var services = scope.ServiceProvider;
         var ecosystemsContext = services.GetRequiredService<EcosystemsContext>();
         var scheduler = services.GetRequiredService<IScheduler>();
         var ecosystems = ecosystemsContext.Ecosystems.Include(ecosystem => ecosystem.Taxonomy).ToList();
         //For all ecosystems: Add taxonomy terms into one List for scheduled mining.
         var miningList = new List<string>();
         foreach (var ecosystem in ecosystems)
         {
            foreach (var tax in ecosystem.Taxonomy)
            {
               miningList.Add(tax.Term);
            }
            //Ensure the ecosystem name is included in the mined topics.
            if (!miningList.Contains(ecosystem.Name))
            {
               miningList.Add(ecosystem.Name);
            }
            scheduler.AddRecurringTaxonomyMiningJob(ecosystem.Name, miningList, 50, 50);
         }
      }
   }
}