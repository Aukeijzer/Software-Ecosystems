using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SECODashBackend.Models;
/// <summary>
/// This class represents a Taxonomy.
/// A Taxonomy is a term that is related to an ecosystem.
/// </summary>
[Index(nameof(Term), IsUnique = true)]
public class Taxonomy
{
    /// <summary>
    /// The term used in taxonomy for software ecosystems.
    /// </summary>
    [DataMember(Name = "term")] 
    [Key]
    public required string Term { get; set; }
    /// <summary>
    /// The ecosystems in which this is term is a taxonomic term.
    /// </summary>
    [DataMember(Name = "ecosystems")]
    public List<Ecosystem> Ecosystems { get; set; } = [];
}