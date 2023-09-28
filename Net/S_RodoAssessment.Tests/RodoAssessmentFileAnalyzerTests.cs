using FluentAssertions;

namespace S_RodoAssessment.Tests
{
    public class RodoAssessmentFileAnalyzerTests
    {
        private readonly IRodoAssessmentFileAnalyzer _sut;

        public RodoAssessmentFileAnalyzerTests()
        {
            _sut = RodoAssessment.Create(RodoAssessment.DevKey);
        }


        [Fact]
        public async Task ProdVersionTakesLongerThen2Minutes()
        {
            //Arrange
            await using var file = new FileStream("./Valid/1.pdf", FileMode.Open);
            var sut = RodoAssessment.Create(RodoAssessment.ProdKey);

            //Act
            var watch = System.Diagnostics.Stopwatch.StartNew();
            await sut.Scan(file);
            watch.Stop();

            //Assert
            watch.Elapsed.Minutes.Should().BeGreaterThan(2);
        }


        [Fact]
        public async Task MatchRodo()
        {
            //Arrange
            await using var file = new FileStream("./Valid/1.pdf", FileMode.Open);

            //Act
            var result = await _sut.Scan(file);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task NotMatchRodo()
        {
            //Arrange
            await using var file = new FileStream("./Valid/2.pdf", FileMode.Open);

            //Act
            var result = await _sut.Scan(file);

            //Assert

            result.Should().BeFalse();
        }
    }
}