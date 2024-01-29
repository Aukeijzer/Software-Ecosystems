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
   }
}