using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Microsoft.EntityFrameworkCore;

namespace SECODashBackend.Models;
/// <summary>
/// This class represents a Technology.
/// A Technology is a term that has been assigned as a technology for a specific ecosystem.
/// </summary>
[Index(nameof(Term), IsUnique = true)]
public class Technology
{
    [DataMember(Name = "term")]
    [Key]
    public required string Term { get; set; }

    [DataMember(Name = "ecosystems")]
    public List<Ecosystem> Ecosystems { get; set; } = [];
}