using FizzWare.NBuilder;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.Mappers.UnitTests
{
    [TestFixture]
    public class CandidateStatusDetailsMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Mapper = new CandidateStatusDetailsMapper();
        }

        public CandidateStatusDetailsMapper Mapper { get; set; }

        [Test]
        public void Map()
        {
            // Arrange
            var candidate = Builder<Candidate>.CreateNew().Build();
            candidate.ContactInfo = Builder<ContactInfo>
                .CreateListOfSize(3)
                .Build()
                ;

            var candidateStatus = Builder<CandidateStatus>
                .CreateNew()
                .Do(row => row.Position =Builder<Position>.CreateNew().Build())
                .Do(row => row.Candidate = candidate)
                .Build()
                ;

            // Act
            var result = this.Mapper.Map(candidateStatus);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.CandidateId, Is.EqualTo(candidateStatus.CandidateId));
            Assert.That(result.CandidateStatusId, Is.EqualTo(candidateStatus.CandidateStatusId));
            Assert.That(result.CandidateName, Is.EqualTo(candidateStatus.Candidate.Name));
            Assert.That(result.PositionTitle, Is.EqualTo(candidateStatus.Position.Title));
            Assert.That(result.PositionId, Is.EqualTo(candidateStatus.Position.PositionId));
            Assert.That(result.Status, Is.EqualTo(candidateStatus.Status));

            for (var i = 0; i < candidate.ContactInfo.Count; i++)
            {
                var expected = candidate.ContactInfo[i];
                var actual = result.ContactInfo[i];
                Assert.That(actual.ContactInfoId, Is.EqualTo(expected.ContactInfoId));
                Assert.That(actual.Type, Is.EqualTo(expected.Type));
                Assert.That(actual.Value, Is.EqualTo(expected.Value));
            }
        }

    }
}
