using System.IO;
using System.Threading.Tasks;
using S_RiskAssesment.Models;

namespace S_RiskAssesment
{
    public interface IRiskAssessmentFileAnalyzer
    {
        public Task<ScanResult> Scan(Stream file);
    }
}