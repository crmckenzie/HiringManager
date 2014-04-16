using System.Linq;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.EntityModel;
using HiringManager.Web.Infrastructure.AutoMapper;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.Mappers.UnitTests.Domain
{
    [TestFixture]
    public class PositionDetailsMapperTests
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
        public void Map_Position()
        {
            // Arrange
            var position = Builder<Position>
                .CreateNew()
                .Build()
                ;

            // Act
            var details = AutoMapper.Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details, Is.Not.Null);
            Assert.That(details.PositionId, Is.EqualTo(position.PositionId));
            Assert.That(details.Title, Is.EqualTo(position.Title));
            Assert.That(details.Candidates, Has.Count.EqualTo(position.Candidates.Count));
        }

        [Test]
        public void Map_PositionDetails()
        {
            // Arrange
            var candidates = Builder<CandidateStatus>
                .CreateListOfSize(10)
                .All()
                .Do(row => row.Candidate = Builder<Candidate>.CreateNew().Build())
                .Do(row => row.Candidate.ContactInfo = Builder<ContactInfo>.CreateListOfSize(3).Build())
                .Build()
                .ToList()
                ;

            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.Candidates = candidates)
                .Build()
                ;

            // Act
            var details = AutoMapper.Mapper.Map<PositionDetails>(position);

            // Assert
            for (var i = 0; i < 10; i++)
            {
                var expected = candidates[i];
                var actual = details.Candidates[i];

                Assert.That(actual.CandidateName, Is.EqualTo(expected.Candidate.Name));
                Assert.That(actual.CandidateStatusId, Is.EqualTo(expected.CandidateStatusId));
                Assert.That(actual.CandidateId, Is.EqualTo(expected.CandidateId));

                Assert.That(actual.ContactInfo, Has.Count.EqualTo(expected.Candidate.ContactInfo.Count));

                for (var j = 0; j < 3; j++)
                {
                    var expectedContactInfo = expected.Candidate.ContactInfo[j];
                    var actualContactInfo = actual.ContactInfo[j];
                    Assert.That(actualContactInfo.Type, Is.EqualTo(expectedContactInfo.Type));
                    Assert.That(actualContactInfo.Value, Is.EqualTo(expectedContactInfo.Value));
                }
            }
        }

        [Test]
        public void CanAddCandidate_WhenPositionOpen()
        {
            // Arrange
            var position = Substitute.For<Position>();
            position.IsFilled().Returns(false);

            // Act
            var details = AutoMapper.Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanAddCandidate, Is.True);
        }

        [Test]
        public void CanAddCandidate_WhenPositionFilled()
        {
            // Arrange
            var position = Substitute.For<Position>();
            position.IsFilled().Returns(true);

            // Act
            var details = AutoMapper.Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanAddCandidate, Is.False);
        }


        [Test]
        public void CanClose_WhenPositionClosed()
        {
            // Arrange
            var position = Substitute.For<Position>();
            position.IsClosed().Returns(true);

            // Act
            var details = AutoMapper.Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanClose, Is.False);
        }

        [Test]
        public void CanClose_WhenPositionIsFilled()
        {
            // Arrange
            var position = Substitute.For<Position>();
            position.IsFilled().Returns(true);

            // Act
            var details = AutoMapper.Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanClose, Is.False);
        }

        [Test]
        public void CanClose_WhenPositionIsNotClosedOrFilled()
        {
            // Arrange
            var position = Substitute.For<Position>();
            position.IsClosed().Returns(false);
            position.IsFilled().Returns(false);

            // Act
            var details = AutoMapper.Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanClose, Is.True);
        }

        [Test]
        public void CanHire_WhenNotHired()
        {
            // Arrange
            var candidates = Builder<CandidateStatus>
                .CreateListOfSize(10)
                .All()
                .Do(row => row.Candidate = Builder<Candidate>.CreateNew().Build())
                .Do(row => row.Candidate.ContactInfo = Builder<ContactInfo>.CreateListOfSize(3).Build())
                .Build()
                .ToList()
                ;


            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.Candidates = candidates)
                .Build()
                ;

            // Act
            var details = AutoMapper.Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanAddCandidate, Is.True);
            foreach (var detail in details.Candidates)
            {
                Assert.That(detail.CanHire, Is.True);
            }
        }

        [Test]
        public void CanSetStatus()
        {
            // Arrange
            var candidates = Builder<CandidateStatus>
                .CreateListOfSize(10)
                .All()
                .Do(row => row.Candidate = Builder<Candidate>.CreateNew().Build())
                .Do(row => row.Candidate.ContactInfo = Builder<ContactInfo>.CreateListOfSize(3).Build())
                .Build()
                .ToList()
                ;


            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.Candidates = candidates)
                .Build()
                ;

            // Act
            var details = AutoMapper.Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanAddCandidate, Is.True);
            foreach (var detail in details.Candidates)
            {
                Assert.That(detail.CanSetStatus, Is.True);
            }
        }

        [Test]
        public void CanPass_WhenNotPassed()
        {
            // Arrange
            var candidates = Builder<CandidateStatus>
                .CreateListOfSize(10)
                .All()
                .Do(row => row.Candidate = Builder<Candidate>.CreateNew().Build())
                .Do(row => row.Candidate.ContactInfo = Builder<ContactInfo>.CreateListOfSize(3).Build())
                .Build()
                .ToList()
                ;


            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.Candidates = candidates)
                .Build()
                ;

            // Act
            var details = AutoMapper.Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanAddCandidate, Is.True);
            foreach (var detail in details.Candidates)
            {
                Assert.That(detail.CanPass, Is.True);
            }
        }

        [Test]
        public void CanPass_WhenPassed()
        {
            // Arrange
            var candidates = Builder<CandidateStatus>
                .CreateListOfSize(10)
                .All()
                .Do(row => row.Candidate = Builder<Candidate>.CreateNew().Build())
                .Do(row => row.Candidate.ContactInfo = Builder<ContactInfo>.CreateListOfSize(3).Build())
                .Build()
                .ToList()
                ;

            var passedOnCandidate = candidates.First();
            passedOnCandidate.Status = "Passed";


            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.Candidates = candidates)
                .Build()
                ;

            // Act
            var details = AutoMapper.Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanAddCandidate, Is.True);
            var detail = details.Candidates.Single(row => row.CandidateId == passedOnCandidate.CandidateId);
            Assert.That(detail.CanPass, Is.False);
        }

        [Test]
        public void CanAddCandidate_WhenPositionIsFilled()
        {
            // Arrange
            var candidates = Builder<CandidateStatus>
                .CreateListOfSize(10)
                .All()
                .Do(row => row.Candidate = Builder<Candidate>.CreateNew().Build())
                .Do(row => row.Candidate.ContactInfo = Builder<ContactInfo>.CreateListOfSize(3).Build())
                .Build()
                .ToList()
                ;

            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.FilledBy = new Candidate())
                .Do(row => row.Candidates = candidates)
                .Build()
                ;

            // Act
            var details = AutoMapper.Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanAddCandidate, Is.False);
            foreach (var detail in details.Candidates)
            {
                Assert.That(detail.CanHire, Is.False);
                Assert.That(detail.CanPass, Is.False);
                Assert.That(detail.CanSetStatus, Is.False);
            }
        }

    }
}
