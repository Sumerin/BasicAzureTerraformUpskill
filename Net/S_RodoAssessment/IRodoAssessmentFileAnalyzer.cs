namespace S_RodoAssessment
{
    public interface IRodoAssessmentFileAnalyzer
    {
        public Task<bool> Scan(Stream file);
    }
}