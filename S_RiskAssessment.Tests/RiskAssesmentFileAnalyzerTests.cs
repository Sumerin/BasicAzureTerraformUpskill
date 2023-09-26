using System;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using S_RiskAssesment;
using S_RiskAssesment.Models;

namespace S_RiskAssessmentTests;

public class RiskAssesmentFileAnalyzerTests
{
    private readonly IRiskAssessmentFileAnalyzer _sut;

    public RiskAssesmentFileAnalyzerTests()
    {
        _sut = RiskAssessment.Create(RiskAssessment.DevKey);
    }

    [Fact]
    public async Task SafeFileWithOneRisk()
    {
        //Arrange
        await using var file = new FileStream("./Valid/1.pdf", FileMode.Open);
        
        //Act
        var result = await _sut.Scan(file);

        //Assert
        result.Filename.Should().Be("AdamCv.pdf");
        result.ThreatPercentage.Should().Be(28);
        result.Status.Should().Be(ThreatStatus.Safe);
        result.Risks.Should().HaveCount(1);
        result.Risks[0].Should().Be("TeamBot");
    }

    [Fact]
    public async Task SuspiciousFileWithTwoRisk()
    {
        //Arrange
        await using var file = new FileStream("./Valid/2.pdf", FileMode.Open);
        
        //Act
        var result = await _sut.Scan(file);

        //Assert
        result.Filename.Should().Be("Jedrek.pdf");
        result.ThreatPercentage.Should().Be(67);
        result.Status.Should().Be(ThreatStatus.Suspicious);
        result.Risks.Should().HaveCount(2);
        result.Risks[0].Should().Be("StormKitty");
        result.Risks[1].Should().Be("SnakeKeylogger");
    }

    [Fact]
    public async Task DangerousFileWithTwoRisk()
    {
        //Arrange
        await using var file = new FileStream("./Valid/3.pdf", FileMode.Open);
        
        //Act
        var result = await _sut.Scan(file);

        //Assert
        result.Filename.Should().Be("est22.pdf");
        result.ThreatPercentage.Should().Be(99);
        result.Status.Should().Be(ThreatStatus.Dangerous);
        result.Risks.Should().HaveCount(2);
        result.Risks[0].Should().Be("RemocosRat");
        result.Risks[1].Should().Be("RedlineStealer");
    }

    [Fact]
    public async Task InvalidFile()
    {
        //Arrange
        await using var file = new FileStream("./InValid/1.pdf", FileMode.Open);

        //Act
        var act = () => _sut.Scan(file);

        //Assert
        await act.Should().ThrowExactlyAsync<ArgumentException>();
    }
}