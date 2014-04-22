using System.Linq;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using HiringManager.Web.Infrastructure.AutoMapper;
using HiringManager.Web.ViewModels;
using HiringManager.Web.ViewModels.Positions;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.AutoMapperProfile
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
        public void PositionSummary()
        {
            // Arrange
            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.Candidates = Builder<CandidateStatus>
                    .CreateListOfSize(10)
                    .TheFirst(4)
                    .Do(candidate => candidate.Status = "Passed")
                    .TheNext(1)
                    .Do(candidate => candidate.Status = "Hired")
                    .Build()
                    .ToList())
                .Build()
                ;

            // Act

            var summary = global::AutoMapper.Mapper.Map<PositionSummary>(position);

            // Assert
            Assert.That(summary.CandidatesAwaitingReview, Is.EqualTo(5));
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
            Assert.That(actual.OpenDate, Is.EqualTo(expected.OpenDate));
            Assert.That(actual.Status, Is.EqualTo(expected.Status));
            Assert.That(actual.Title, Is.EqualTo(expected.Title));
        }

    }
}
