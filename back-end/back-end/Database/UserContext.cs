using Microsoft.EntityFrameworkCore;
using SECODashBackend.Models;

namespace SECODashBackend.Database;

/// <summary>
/// Represents the database context for the User entity.
/// </summary>
public class UserContext(DbContextOptions<UserContext> options) : DbContext(options)
{
    /// <summary>
    /// Configures the model for the context.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
    public DbSet<User> Users { get; set; }
    
}