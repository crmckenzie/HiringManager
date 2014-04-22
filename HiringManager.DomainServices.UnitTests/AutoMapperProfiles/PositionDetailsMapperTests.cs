using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FizzWare.NBuilder;
using HiringManager.DomainServices.AutoMapperProfiles;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.UnitTests.AutoMapperProfiles
{
    [TestFixture]
    public class PositionDetailsMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
            Mapper.Reset();
            Mapper.AddProfile<DomainProfile>();
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
        }

        [Test]
        public void Map_PositionDetails()
        {
            // Arrange
            var position = Builder<Position>
                .CreateNew()
                .Build()
                ;

            // Act
            var details = Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details, Is.Not.Null);
            Assert.That(details.PositionId, Is.EqualTo(position.PositionId));
            Assert.That(details.Title, Is.EqualTo(position.Title));
            Assert.That(details.Candidates, Has.Count.EqualTo(position.Candidates.Count));
        }

        [Test]
        public void Map_CandidateStatus()
        {
            // Arrange
            var candidates = Builder<CandidateStatus>
                .CreateListOfSize(10)
                .All()
                .Do(row => row.Candidate = Builder<Candidate>.CreateNew().Build())
                .Do(row => row.Candidate.ContactInfo = Builder<ContactInfo>.CreateListOfSize(3).Build())
                .Do(row => row.Candidate.Source = new Source()
                                                  {
                                                      SourceId = 1,
                                                      Name = "Some Source",
                                                  })
                .Build()
                .ToList()
                ;

            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.Candidates = candidates)
                .Do(row =>
                    {
                        foreach (var candidate in row.Candidates)
                            candidate.Position = row;
                    })
                .Do(row => row.Openings = Builder<Opening>.CreateListOfSize(3).Build().ToList())
                .Build()
                ;

            position.Openings.First().FilledBy = position.Candidates.First().Candidate;

            // Act
            var details = Mapper.Map<PositionDetails>(position);
            Assert.That(details.Openings, Is.EqualTo(3));
            Assert.That(details.OpeningsFilled, Is.EqualTo(1));

            // Assert
            for (var i = 0; i < 10; i++)
            {
                var expected = candidates[i];
                var actual = details.Candidates[i];

                Assert.That(actual.CandidateName, Is.EqualTo(expected.Candidate.Name));
                Assert.That(actual.CandidateStatusId, Is.EqualTo(expected.CandidateStatusId));
                Assert.That(actual.CandidateId, Is.EqualTo(expected.CandidateId));

                Assert.That(actual.ContactInfo, Has.Count.EqualTo(expected.Candidate.ContactInfo.Count));

                Assert.That(actual.SourceId, Is.EqualTo(expected.Candidate.Source.SourceId));
                Assert.That(actual.Source, Is.EqualTo(expected.Candidate.Source.Name));

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
            position.Openings.Returns(new List<Opening>());
            position.IsFilled().Returns(false);

            // Act
            var details = Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanAddCandidate, Is.True);
        }

        [Test]
        public void CanAddCandidate_WhenPositionFilled()
        {
            // Arrange
            var position = Substitute.For<Position>();
            position.Openings.Returns(new List<Opening>());
            position.IsFilled().Returns(true);

            // Act
            var details = Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanAddCandidate, Is.False);
        }

        [Test]
        public void BasicMap()
        {
            // Arrange
            var position = Substitute.For<Position>();
            position.Openings.Returns(new List<Opening>());

            // Act
            var details = Mapper.Map<Position, PositionDetails>(position);

            // Assert
            Assert.That(details, Is.Not.Null);
        }

        [Test]
        public void CanClose_WhenPositionClosed()
        {
            // Arrange
            var position = Substitute.For<Position>();
            position.IsClosed().Returns(true);
            position.Openings.Returns(new List<Opening>());

            // Act
            var details = Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanClose, Is.False);
        }

        [Test]
        public void CanClose_WhenPositionIsFilled()
        {
            // Arrange
            var position = Substitute.For<Position>();
            position.Openings.Returns(new List<Opening>());
            position.IsFilled().Returns(true);

            // Act
            var details = Mapper.Map<PositionDetails>(position);

            // Assert
            Assert.That(details.CanClose, Is.False);
        }

        [Test]
        public void CanClose_WhenPositionIsNotClosedOrFilled()
        {
            // Arrange
            var position = Substitute.For<Position>();
            position.Openings.Returns(new List<Opening>());
            position.IsClosed().Returns(false);
            position.IsFilled().Returns(false);

            // Act
            var details = Mapper.Map<PositionDetails>(position);

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
                .Do(row =>
                    {
                        foreach (var candidate in row.Candidates)
                            candidate.Position = row;
                    })
                .Build()
                ;

            // Act
            var details = Mapper.Map<PositionDetails>(position);

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
                .Do(row =>
                    {
                        foreach (var candidate in row.Candidates)
                            candidate.Position = row;
                    })
                .Build()
                ;

            // Act
            var details = Mapper.Map<PositionDetails>(position);

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
                .Do(row =>
                    {
                        foreach (var candidate in row.Candidates)
                            candidate.Position = row;
                    })
                .Build()
                ;

            // Act
            var details = Mapper.Map<PositionDetails>(position);

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
                .Do(row =>
                    {
                        foreach (var candidate in row.Candidates)
                            candidate.Position = row;
                    })
                .Build()
                ;

            // Act
            var details = Mapper.Map<PositionDetails>(position);

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
                .Do(row => row.Openings.Add(new Opening() { FilledBy = new Candidate() }))
                .Do(row => row.Candidates = candidates)
                .Do(row =>
                    {
                        foreach (var candidate in row.Candidates)
                            candidate.Position = row;
                    })
                .Build()
                ;

            // Act
            var details = Mapper.Map<PositionDetails>(position);

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