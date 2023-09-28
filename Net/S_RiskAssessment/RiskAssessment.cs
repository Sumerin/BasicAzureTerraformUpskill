using System;

namespace S_RiskAssesment
{
    public static class RiskAssessment
    {
        internal const string DevKey = "BDD3CBE2-4692-4E74-BF19-79444229343B";
        internal const string ProdKey = "506333AD-F056-4085-9FDC-06A9D87D3683";

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