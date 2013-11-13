using FizzWare.NBuilder;
using HiringManager.EntityModel;
using NUnit.Framework;

namespace HiringManager.Mappers.UnitTests
{
    [TestFixture]
    public class PositionSummaryMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Mapper = new PositionSummaryMapper();
        }

        public PositionSummaryMapper Mapper { get; set; }

        [Test]
        public void Map()
        {
            // Arrange
            var candidateStatuses = Builder<CandidateStatus>
                .CreateListOfSize(10)
                .TheFirst(4)
                    .Do(row => row.Status = "Resume Received")
                .TheNext(2)
                    .Do(row => row.Status = "Passed")
                .TheNext(2)
                    .Do(row => row.Status = "Phone Screen Scheduled")
                .TheNext(1)
                    .Do(row => row.Status = "Interview Scheduled")
                .TheNext(1)
                    .Do(row => row.Status = "Hired")
                .Build()
                ;
            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.CreatedBy = Builder<Manager>.CreateNew().Build())
                .Do(row =>
                    {
                        row.Candidates = candidateStatuses;
                    })
                .Build()
                ;

            // Act
            var result = this.Mapper.Map(position);

            // Assert
            Assert.That(result.PositionId, Is.EqualTo(position.PositionId));
            Assert.That(result.CreatedByName, Is.EqualTo(position.CreatedBy.Name));
            Assert.That(result.CreatedById, Is.EqualTo(position.CreatedById));
            Assert.That(result.FilledDate, Is.EqualTo(position.FilledDate));
            Assert.That(result.OpenDate, Is.EqualTo(position.OpenDate));
            Assert.That(result.Status, Is.EqualTo(position.Status));
            Assert.That(result.Title, Is.EqualTo(position.Title));
        }

        [Test]
        public void Map_HiredAndPassedAreNotCountedInCandidatesAwaitingReview()
        {
            // Arrange
            var candidateStatuses = Builder<CandidateStatus>
                .CreateListOfSize(10)
                .TheFirst(4)
                    .Do(row => row.Status = "Resume Received")
                .TheNext(2)
                    .Do(row => row.Status = "Passed")
                .TheNext(2)
                    .Do(row => row.Status = "Phone Screen Scheduled")
                .TheNext(1)
                    .Do(row => row.Status = "Interview Scheduled")
                .TheNext(1)
                    .Do(row => row.Status = "Hired")
                .Build()
                ;
            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.CreatedBy = Builder<Manager>.CreateNew().Build())
                .Do(row =>
                {
                    row.Candidates = candidateStatuses;
                })
                .Build()
                ;

            // Act
            var result = this.Mapper.Map(position);

            // Assert
            Assert.That(result.PositionId, Is.EqualTo(position.PositionId));
            Assert.That(result.CreatedByName, Is.EqualTo(position.CreatedBy.Name));
            Assert.That(result.CreatedById, Is.EqualTo(position.CreatedById));
            Assert.That(result.FilledDate, Is.EqualTo(position.FilledDate));
            Assert.That(result.OpenDate, Is.EqualTo(position.OpenDate));
            Assert.That(result.Status, Is.EqualTo(position.Status));
            Assert.That(result.Title, Is.EqualTo(position.Title));
            Assert.That(result.CandidatesAwaitingReview, Is.EqualTo(7));
        }

        [Test]
        public void Map_FilledPosition_CandidatesAwaitingReviewShoudlBeZero()
        {
            // Arrange
            var candidateStatuses = Builder<CandidateStatus>
                .CreateListOfSize(10)
                .TheFirst(4)
                    .Do(row => row.Status = "Resume Received")
                .TheNext(2)
                    .Do(row => row.Status = "Passed")
                .TheNext(2)
                    .Do(row => row.Status = "Phone Screen Scheduled")
                .TheNext(1)
                    .Do(row => row.Status = "Interview Scheduled")
                .TheNext(1)
                    .Do(row => row.Status = "Hired")
                .Build()
                ;
            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.CreatedBy = Builder<Manager>.CreateNew().Build())
                .Do(row =>
                {
                    row.Candidates = candidateStatuses;
                })
                .Do(row => row.Status = "Filled")
                .Build()
                ;

            // Act
            var result = this.Mapper.Map(position);

            // Assert
            Assert.That(result.PositionId, Is.EqualTo(position.PositionId));
            Assert.That(result.CreatedByName, Is.EqualTo(position.CreatedBy.Name));
            Assert.That(result.CreatedById, Is.EqualTo(position.CreatedById));
            Assert.That(result.FilledDate, Is.EqualTo(position.FilledDate));
            Assert.That(result.OpenDate, Is.EqualTo(position.OpenDate));
            Assert.That(result.Status, Is.EqualTo(position.Status));
            Assert.That(result.Title, Is.EqualTo(position.Title));
            Assert.That(result.CandidatesAwaitingReview, Is.EqualTo(0));
        }
    }
}
