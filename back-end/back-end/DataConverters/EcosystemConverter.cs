using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Models;

namespace SECODashBackend.DataConverters;

public static class EcosystemConverter
{
    public static EcosystemOverviewDto ToDto(Ecosystem ecosystem)
    {
        return new EcosystemOverviewDto
        {
            Description = ecosystem.Description,
            DisplayName = ecosystem.DisplayName,
            NumberOfStars = ecosystem.NumberOfStars,
        };
    }
}