using System.Linq;
using AutoMapper.QueryableExtensions;
using FizzWare.NBuilder;
using HiringManager.DomainServices.AutoMapperProfiles;
using HiringManager.EntityModel;
using NUnit.Framework;

namespace HiringManager.DomainServices.UnitTests.AutoMapperProfiles
{
    [TestFixture]
    public class PositionSummaryMappingTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.AddProfile<DomainProfile>();
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
            Assert.That((object)result, Is.Not.Null);
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
            Assert.That((object)result.PositionId, Is.EqualTo(position.PositionId));
            Assert.That((object)result.CreatedByName, Is.EqualTo(position.CreatedBy.Name));
            Assert.That((object)result.CreatedById, Is.EqualTo(position.CreatedById));
            Assert.That((object)result.FilledByCandidateId, Is.Null);
            Assert.That((object)result.FilledByName, Is.Null);
            Assert.That((object)result.FilledDate, Is.EqualTo(position.FilledDate));
            Assert.That((object)result.OpenDate, Is.EqualTo(position.OpenDate));
            Assert.That((object)result.Status, Is.EqualTo(position.Status));
            Assert.That((object)result.Title, Is.EqualTo(position.Title));
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
            Assert.That((object)result.PositionId, Is.EqualTo(position.PositionId));
            Assert.That((object)result.CreatedByName, Is.EqualTo(position.CreatedBy.Name));
            Assert.That((object)result.CreatedById, Is.EqualTo(position.CreatedById));
            Assert.That((object)result.FilledByCandidateId, Is.EqualTo(position.FilledBy.CandidateId));
            Assert.That((object)result.FilledByName, Is.EqualTo(position.FilledBy.Name));
            Assert.That((object)result.FilledDate, Is.EqualTo(position.FilledDate));
            Assert.That((object)result.OpenDate, Is.EqualTo(position.OpenDate));
            Assert.That((object)result.Status, Is.EqualTo(position.Status));
            Assert.That((object)result.Title, Is.EqualTo(position.Title));
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
            Assert.That((object)result.PositionId, Is.EqualTo(position.PositionId));
            Assert.That((object)result.CreatedByName, Is.EqualTo(position.CreatedBy.Name));
            Assert.That((object)result.CreatedById, Is.EqualTo(position.CreatedById));
            Assert.That((object)result.FilledDate, Is.EqualTo(position.FilledDate));
            Assert.That((object)result.OpenDate, Is.EqualTo(position.OpenDate));
            Assert.That((object)result.Status, Is.EqualTo(position.Status));
            Assert.That((object)result.Title, Is.EqualTo(position.Title));
            Assert.That((object)result.CandidatesAwaitingReview, Is.EqualTo(7));
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
            Assert.That((object)result.PositionId, Is.EqualTo(position.PositionId));
            Assert.That((object)result.CreatedByName, Is.EqualTo(position.CreatedBy.Name));
            Assert.That((object)result.CreatedById, Is.EqualTo(position.CreatedById));
            Assert.That((object)result.FilledDate, Is.EqualTo(position.FilledDate));
            Assert.That((object)result.OpenDate, Is.EqualTo(position.OpenDate));
            Assert.That((object)result.Status, Is.EqualTo(position.Status));
            Assert.That((object)result.Title, Is.EqualTo(position.Title));
            Assert.That((object)result.CandidatesAwaitingReview, Is.EqualTo(0));
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
