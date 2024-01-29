﻿using SECODashBackend.Dtos.Ecosystem;
using SECODashBackend.Models;

namespace SECODashBackend.DataConverters;

/// <summary>
/// Converts Ecosystems to and from data transfer objects.
/// </summary> 
public static class EcosystemConverter
{
    /// <summary>
    /// Converts an Ecosystem to a data transfer object.
    /// </summary>
    public static EcosystemOverviewDto ToDto(Ecosystem ecosystem)
    {
        return new EcosystemOverviewDto
        {
            Name = ecosystem.Name,
            Description = ecosystem.Description,
            DisplayName = ecosystem.DisplayName,
            NumberOfStars = ecosystem.NumberOfStars,
        };
    }
}