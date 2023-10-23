using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Models;

namespace SECODashBackend.DataConverters;

public static class EcosystemConverter
{
    public static EcosystemDto ToDto(Ecosystem ecosystem)
    {
        return new EcosystemDto
        {
            Id = ecosystem.Id,
            Description = ecosystem.Description,
            DisplayName = ecosystem.DisplayName,
            Name = ecosystem.Name,
            NumberOfStars = ecosystem.NumberOfStars,
        };
    }
}