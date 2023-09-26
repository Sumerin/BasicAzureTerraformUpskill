using System;

namespace S_RiskAssesment
{
    public static class RiskAssessment
    {
        internal const string DevKey = "";
        internal const string ProdKey = "";

        public static IRiskAssessmentFileAnalyzer Create(string licenseKey)
        {

            if (licenseKey == DevKey)
            {
                return new RiskAssessmentFileAnalyzer(true);
            }
              
            if (licenseKey == ProdKey)
            {
                return new RiskAssessmentFileAnalyzer(false);
            }

            throw new ArgumentException("Invalid Key", nameof(licenseKey));

        }
    }
}