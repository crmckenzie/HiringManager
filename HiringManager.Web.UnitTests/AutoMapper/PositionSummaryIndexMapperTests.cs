using System.Linq;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.Web.Infrastructure.AutoMapper;
using HiringManager.Web.ViewModels;
using HiringManager.Web.ViewModels.Positions;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.AutoMapper
{
    [TestFixture]
    public class PositionSummaryIndexMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
            AutoMapperConfiguration.Configure();
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
        }

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
            var result = global::AutoMapper.Mapper.Map<IndexViewModel<PositionSummaryIndexItem>>(queryResponse);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Data, Is.Not.Null);
            Assert.That(result.Data, Has.Count.EqualTo(10));

            var expected = queryResponse.Data.First();
            var actual = result.Data.First();

            Assert.That(actual.CandidatesAwaitingReview, Is.EqualTo(expected.CandidatesAwaitingReview));
            Assert.That(actual.CreatedByName, Is.EqualTo(expected.CreatedByName), "CreatedByName");
            Assert.That(actual.FilledByCandidateId, Is.EqualTo(expected.FilledByCandidateId));
            Assert.That(actual.FilledByName, Is.EqualTo(expected.FilledByName));
            Assert.That(actual.FilledDate, Is.EqualTo(expected.FilledDate));
            Assert.That(actual.OpenDate, Is.EqualTo(expected.OpenDate));
            Assert.That(actual.Status, Is.EqualTo(expected.Status));
            Assert.That(actual.Title, Is.EqualTo(expected.Title));
        }

    }
}
