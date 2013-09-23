using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.Domain;
using NUnit.Framework;

namespace HiringManager.Mappers.UnitTests
{
    [TestFixture]
    public class PositionDetailsMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Mapper = new PositionDetailsMapper();
        }

        public PositionDetailsMapper Mapper { get; set; }

        [Test]
        public void Map()
        {
            // Arrange
            var candidates = Builder<CandidateStatus>
                .CreateListOfSize(10)
                .All()
                .Do(row => row.Candidate = Builder<Candidate>.CreateNew().Build())
                .Do(row => row.Candidate.ContactInfo = Builder<ContactInfo>.CreateListOfSize(3).Build())
                .Build()
                ;

            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.Candidates = candidates)
                .Build()
                ;

            // Act
            var details = this.Mapper.Map(position);

            // Assert
            Assert.That(details, Is.Not.Null);
            Assert.That(details.PositionId, Is.EqualTo(position.PositionId));
            Assert.That(details.Title, Is.EqualTo(position.Title));
            Assert.That(details.Candidates, Has.Count.EqualTo(position.Candidates.Count));

            for (var i = 0; i < 10; i++)
            {
                var expected = candidates[i];
                var actual = details.Candidates[i];

                Assert.That(actual.Name, Is.EqualTo(expected.Candidate.Name));
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

    }
}
