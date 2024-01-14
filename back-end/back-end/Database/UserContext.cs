using Microsoft.EntityFrameworkCore;
using SECODashBackend.Models;

namespace SECODashBackend.Database;

/// <summary>
/// Represents the database context for the User entity.
/// </summary>
public class UserContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserContext"/> class.
    /// </summary>
    /// <param name="options">The options for configuring the context.</param>
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {
    }

    /// <summary>
    /// Configures the model for the context.
    /// </summary>
    /// <param name="modelBuilder">The builder used to construct the model for this context.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }

    /// <summary>
    /// Gets or sets the collection of users.
    /// </summary>
    public DbSet<User> Users { get; set; }
}