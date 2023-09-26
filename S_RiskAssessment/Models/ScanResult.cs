using System.Collections.ObjectModel;

namespace S_RiskAssesment.Models
{
    public record ScanResult(
        string Filename,
        ThreatStatus Status,
        int ThreatPercentage, 
        ReadOnlyCollection<string> Risks);
}