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
    [DataMember(Name = "term")] 
    [Key]
    public required string Term { get; set; }

    [DataMember(Name = "ecosystems")]
    public List<Ecosystem> Ecosystems { get; set; } = [];
}