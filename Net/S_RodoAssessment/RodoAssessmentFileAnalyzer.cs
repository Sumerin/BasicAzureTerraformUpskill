using System.Security.Cryptography;
using PdfSharp.Pdf.IO;

namespace S_RodoAssessment
{
    internal class RodoAssessmentFileAnalyzer : IRodoAssessmentFileAnalyzer
    {
        private const char TagSeparator = ';';
        private const char KeyValueSeparator = '=';

        private readonly bool _useTestMode;
        private const int MinimumRunningTime = 2;
        private const int MaximumRunningTime = 4;

        private static class TagKey
        {
            internal const string Rodo = "rodo";
        }

        internal RodoAssessmentFileAnalyzer(bool useTestMode)
        {
            _useTestMode = useTestMode;
        }

        public async Task<bool> Scan(Stream file)
        {
            var tags = GetTags(file);

            var result = bool.Parse(tags[TagKey.Rodo]);

            if (!_useTestMode)
            {
                await SimulateLongRun();
            }

            return result;
        }

        private static Dictionary<string, string> GetTags(Stream file)
        {
            var pdf = PdfReader.Open(file, PdfDocumentOpenMode.ReadOnly);
            var keyValueTags = pdf.Info.Keywords.Split(TagSeparator);
            return keyValueTags.Select(SplitTagByEqualSign).ToDictionary(x => x[0], x => x[1]);
        }

        private static string[] SplitTagByEqualSign(string tag)
        {
            if (ContainsExactlyOneEqualSign(tag, out var split))
            {
                return split;
            }

            throw new ArgumentException($"Tag: {tag} should contain exactly one \'{KeyValueSeparator}\'", nameof(tag));
        }

        private static bool ContainsExactlyOneEqualSign(string tag, out string[] split)
        {
            split = tag.Split(KeyValueSeparator);
            return split.Length == 2;
        }

        private static async Task SimulateLongRun()
        {
            var runningTime = RandomNumberGenerator.GetInt32(MinimumRunningTime, MaximumRunningTime);
            // We want to occupy thread??
            await Task.Delay(TimeSpan.FromMinutes(runningTime));
            //Thread.Sleep(TimeSpan.FromMinutes(runningTime));
        }
    }
}