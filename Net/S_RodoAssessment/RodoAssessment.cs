namespace S_RodoAssessment
{
    public static class RodoAssessment
    {
        internal const string DevKey = "E15EF5C8-5172-418D-9A2E-E04C300C34D9";
        internal const string ProdKey = "84052DC8-0D90-4DA8-B007-16AA227E908F";

        public static IRodoAssessmentFileAnalyzer Create(string licenseKey)
        {
            if (licenseKey == DevKey)
            {
                return new RodoAssessmentFileAnalyzer(true);
            }

            if (licenseKey == ProdKey)
            {
                return new RodoAssessmentFileAnalyzer(false);
            }

            throw new ArgumentException("Invalid Key", nameof(licenseKey));
        }
    }
}