using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.EntityModel;
using HiringManager.Web.Infrastructure.AutoMapper;
using NUnit.Framework;

namespace HiringManager.Mappers.UnitTests.Domain
{
    [TestFixture]
    public class PositionSummaryMappingTests
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
        public void RawMap()
        {
            // Arrange
            var source = new Position();

            // Act
            var result = AutoMapper.Mapper.Map<PositionSummary>(source);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

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
                .ToList()
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
            var result = AutoMapper.Mapper.Map<PositionSummary>(position);

            // Assert
            Assert.That(result.PositionId, Is.EqualTo(position.PositionId));
            Assert.That(result.CreatedByName, Is.EqualTo(position.CreatedBy.Name));
            Assert.That(result.CreatedById, Is.EqualTo(position.CreatedById));
            Assert.That(result.FilledByCandidateId, Is.Null);
            Assert.That(result.FilledByName, Is.Null);
            Assert.That(result.FilledDate, Is.EqualTo(position.FilledDate));
            Assert.That(result.OpenDate, Is.EqualTo(position.OpenDate));
            Assert.That(result.Status, Is.EqualTo(position.Status));
            Assert.That(result.Title, Is.EqualTo(position.Title));
        }

        [Test]
        public void Map_WhenPositionIsFilled()
        {
            // Arrange
            var yourHired = new Candidate();
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
                    .Do(row =>
                        {
                            row.Candidate = yourHired;
                        })
                .Build()
                .ToList()
                ;

            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.CreatedBy = Builder<Manager>.CreateNew().Build())
                .Do(row =>
                {
                    row.Candidates = candidateStatuses;
                })
                .Do(row => row.FilledBy = candidateStatuses.Last().Candidate)
                .Build()
                ;

            // Act
            var result = AutoMapper.Mapper.Map<PositionSummary>(position);

            // Assert
            Assert.That(result.PositionId, Is.EqualTo(position.PositionId));
            Assert.That(result.CreatedByName, Is.EqualTo(position.CreatedBy.Name));
            Assert.That(result.CreatedById, Is.EqualTo(position.CreatedById));
            Assert.That(result.FilledByCandidateId, Is.EqualTo(position.FilledBy.CandidateId));
            Assert.That(result.FilledByName, Is.EqualTo(position.FilledBy.Name));
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
                .ToList()
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
            var result = AutoMapper.Mapper.Map<PositionSummary>(position);

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
                .ToList()
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
            var result = AutoMapper.Mapper.Map<PositionSummary>(position);

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

        [Test]
        public void Project_To()
        {
            // Arrange
            var positions = Builder<Position>
                .CreateListOfSize(10)
                .All()
                .Do(row => row.CreatedBy = Builder<Manager>.CreateNew().Build())
                .Do(row => row.FilledBy = Builder<Candidate>.CreateNew().Build())
                .Build()
                ;

            // Act
            foreach (var position in positions)
            {
                AutoMapper.Mapper.Map<PositionSummary>(position);
            }



            var query = positions.AsQueryable()
                .Project().To<PositionSummary>()
                .ToList()
                ;

            // Assert
        }
    }
}
