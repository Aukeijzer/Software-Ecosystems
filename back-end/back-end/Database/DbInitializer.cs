using Elastic.Clients.Elasticsearch.Analysis;
using SECODashBackend.Models;
using SECODashBackend.Services.Ecosystems;

namespace SECODashBackend.Database;

/// <summary>
/// Database initializer.
/// </summary>
public static class DbInitializer
{
   /// <summary>
   /// Initializes the Ecosystems database with the top-level ecosystems.
   /// </summary>
   /// <param name="context">The database context.</param>
   /// <param name="ecosystemsService"></param>
   public static void Initialize(EcosystemsContext context, IEcosystemsService ecosystemsService)
   {
      var ecosystem = context.Ecosystems.FirstOrDefault();
      if (ecosystem != null) return;
      Task.Run(() => ecosystemsService.CreateEcosystem(InitialDatabases.agriculture)).Wait();
      Task.Run(() => ecosystemsService.UpdateTopics(InitialDatabases.agriculture)).Wait();
      Task.Run(() => ecosystemsService.CreateEcosystem(InitialDatabases.quantum)).Wait();
      Task.Run(() => ecosystemsService.UpdateTopics(InitialDatabases.quantum)).Wait();
      Task.Run(() => ecosystemsService.CreateEcosystem(InitialDatabases.artificialintelligence)).Wait();
      Task.Run(() => ecosystemsService.UpdateTopics(InitialDatabases.artificialintelligence)).Wait();
      //context.Ecosystems.Add(new Ecosystem
      // {
      //    Id = Guid.NewGuid().ToString(),
      //    Name = "agriculture",
      //    DisplayName = "Agriculture",
      //    Description = "Software related to agriculture",
      //    NumberOfStars = 34565,
      //    Users = [context.Users.Find("InitialRootAdmin")]
      // });
      // context.Ecosystems.Add(new Ecosystem
      // {
      //    Id = Guid.NewGuid().ToString(), 
      //    Name = "quantum",
      //    DisplayName = "Quantum",
      //    Description = "Software related to quantum mechanics",
      //    NumberOfStars = 4352,
      //    Users = [context.Users.Find("3898088433")],
      // });
      // context.Ecosystems.Add(new Ecosystem
      // {
      //    Id = Guid.NewGuid().ToString(),
      //    Name = "artificial-intelligence",
      //    DisplayName = "Artificial Intelligence",
      //    Description = "Software related to artificial intelligence",
      //    NumberOfStars = 4352,
      //    Users = [context.Users.Find("3898088433")],
      // });
      // context.SaveChanges();
   }
}