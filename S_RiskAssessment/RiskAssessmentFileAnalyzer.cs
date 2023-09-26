using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using PdfSharp.Pdf.IO;
using S_RiskAssesment.Models;

namespace S_RiskAssesment;

internal class RiskAssessmentFileAnalyzer : IRiskAssessmentFileAnalyzer
{
    private const char TagSeparator = ';';
    private const char KeyValueSeparator = '=';

    private readonly bool _useTestMode;
    private const int MinimumRunningTime = 2;
    private const int MaximumRunningTime = 4;

    private static class TagKey
    {
        internal const string Filename = "filename";
        internal const string Status = "status";
        internal const string ThreatPercentage = "threatPercentage";
        internal const string RiskPrefix = "risk";
    }

    internal RiskAssessmentFileAnalyzer(bool useTestMode)
    {
        _useTestMode = useTestMode;
    }

    public async Task<ScanResult> Scan(Stream file)
    {
        var tags = GetTags(file);

        var risks = ExtractRisks(tags);

        var result = new ScanResult(
            Filename: tags[TagKey.Filename],
            Status: Enum.Parse<ThreatStatus>(tags[TagKey.Status]),
            ThreatPercentage: int.Parse(tags[TagKey.ThreatPercentage]),
            Risks: risks);

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

    private static ReadOnlyCollection<string> ExtractRisks(Dictionary<string, string> dictionary)
    {
        var risks = dictionary.Where(x => x.Key.StartsWith(TagKey.RiskPrefix))
            .Select(x => x.Value)
            .ToList()
            .AsReadOnly();
        return risks;
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