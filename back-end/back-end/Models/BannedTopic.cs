using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
namespace SECODashBackend.Models;

/// <summary>
/// A banned topic is a topic the creator of an ecosystem has flagged to not be shown on the dashboard.
/// </summary>
public class BannedTopic
{
    /// <summary>
    /// The term used in taxonomy for software ecosystems.
    /// </summary>
    [DataMember(Name = "term")] 
    [Key]
    public required string Term { get; set; }
    /// <summary>
    /// The ecosystems in which this is term is a banned topic.
    /// </summary>
    [DataMember(Name = "ecosystems")]
    public List<Ecosystem> Ecosystems { get; set; } = [];
}