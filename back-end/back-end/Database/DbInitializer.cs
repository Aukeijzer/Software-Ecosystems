using SECODashBackend.Models;

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
   public static void Initialize(EcosystemsContext context)
   {
      var ecosystem = context.Ecosystems.FirstOrDefault();
      if (ecosystem != null) return;

      context.Ecosystems.Add(new Ecosystem
      {
         Id = Guid.NewGuid().ToString(),
         Name = "agriculture",
         DisplayName = "Agriculture",
         Description = "Software related to agriculture",
         NumberOfStars = 34565,
         Technologies = ["farming"],
      });
      context.Ecosystems.Add(new Ecosystem
      {
         Id = Guid.NewGuid().ToString(), 
         Name = "quantum",
         DisplayName = "Quantum",
         Description = "Software related to quantum mechanics",
         NumberOfStars = 4352,
         Technologies = ["qubits"],
      });
      context.Ecosystems.Add(new Ecosystem
      {
         Id = Guid.NewGuid().ToString(),
         Name = "artificial-intelligence",
         DisplayName = "Artificial Intelligence",
         Description = "Software related to artificial intelligence",
         NumberOfStars = 4352,
         Technologies = ["neural-networks"],
      });
      context.SaveChanges();
   }
}