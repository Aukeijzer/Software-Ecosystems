using Microsoft.EntityFrameworkCore;
using SECODashBackend.Models;
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
         UserDbInitializer.Initialize(ecosystemsContext);
         DbInitializer.Initialize(ecosystemsContext);
      }
   }

   public static void ScheduleInitialJobs(this IHost host)
   {
      using (var scope = host.Services.CreateScope())
      {
         var services = scope.ServiceProvider;
         var ecosystemsContext = services.GetRequiredService<EcosystemsContext>();
         var scheduler = services.GetRequiredService<HangfireScheduler>();
         var ecosystems = ecosystemsContext.Ecosystems.Include("Taxonomy").Include(ecosystem => ecosystem.Technologies);
         var miningList = new List<string>();
         foreach (var ecosystem in ecosystems)
         {
            foreach (var tax in ecosystem.Taxonomy)
            {
               miningList.Add(tax.Term);
            }

            foreach (var tech in ecosystem.Technologies)
            {
               miningList.Add(tech.Term);
            }

            scheduler.AddRecurringTaxonomyMiningJob(ecosystem.Name, miningList, 50, 50);
         }
      }
   }
}