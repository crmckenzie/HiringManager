using System.Linq;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.Web.Mappers.Positions;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.Mappers
{
    [TestFixture]
    public class PositionSummaryIndexMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Mapper = new PositionSummaryIndexMapper();
        }

        public PositionSummaryIndexMapper Mapper { get; set; }

        [Test]
        public void Map()
        {
            // Arrange
            var queryResponse = new QueryResponse<PositionSummary>()
                                {
                                    Data = Builder<PositionSummary>
                                        .CreateListOfSize(10)
                                        .Build()
                                        
                                };

            // Act
            var result = this.Mapper.Map(queryResponse);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data, Has.Count.EqualTo(10));

            var expected = queryResponse.Data.First();
            var actual = result.Data.First();

            Assert.That(actual.CandidatesAwaitingReview, Is.EqualTo(expected.CandidatesAwaitingReview));
            Assert.That(actual.OpenDate, Is.EqualTo(expected.OpenDate));
            Assert.That(actual.CreatedByName, Is.EqualTo(expected.CreatedByName), "CreatedByName");
            Assert.That(actual.Title, Is.EqualTo(expected.Title));
            Assert.That(actual.Status, Is.EqualTo(expected.Status));
        }

    }
}
