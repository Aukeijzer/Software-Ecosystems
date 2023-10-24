using Microsoft.EntityFrameworkCore;
using SECODashBackend.Models;

namespace SECODashBackend.Database;

public class EcosystemsContext: DbContext
{
   public EcosystemsContext(DbContextOptions<EcosystemsContext> options) : base(options)
   {
   }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      modelBuilder.UseSerialColumns();
   }
   public DbSet<Ecosystem> Ecosystems { get; set; }
}