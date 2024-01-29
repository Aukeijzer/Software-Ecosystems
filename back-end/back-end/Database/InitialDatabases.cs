using SECODashBackend.Dtos.Ecosystem;

namespace SECODashBackend.Database;

public static class InitialDatabases
{
    public static EcosystemCreationDto agriculture = new EcosystemCreationDto
    {
        EcosystemName = "agriculture",
        Description = "Software related to agriculture",
        Email = "3898088433",
        Topics = 
        [
            "Agriculture", 
            "Farming", 
            "Rural development",
            "Agro-tech",
            "Sustainable practices",
            "Crop analysis",
            "Agricultural innovation",
            "Agri-data",
            "Open-source farming",
            "Smart agriculture",
            "Cows",
            "Livestock" 
        ],
        Technologies = [],
        Excluded = [],
    };
    
    
    private static EcosystemCreationDto artificialintelligence;
    private static EcosystemCreationDto quantum;

}