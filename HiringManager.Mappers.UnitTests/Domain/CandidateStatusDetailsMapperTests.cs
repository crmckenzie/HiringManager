using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.EntityModel;
using HiringManager.Web.Infrastructure.AutoMapper;
using NUnit.Framework;

namespace HiringManager.Mappers.UnitTests.Domain
{
    [TestFixture]
    public class CandidateStatusDetailsMapperTests
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
            var candidate = Builder<Candidate>.CreateNew().Build();
            candidate.ContactInfo = Builder<ContactInfo>
                .CreateListOfSize(3)
                .Build()
                ;

            var candidateStatus = Builder<CandidateStatus>
                .CreateNew()
                .Do(row => row.Position = Builder<Position>.CreateNew().Build())
                .Do(row => row.Candidate = candidate)
                .Build()
                ;

            // Act
            var result = AutoMapper.Mapper.Map<CandidateStatusDetails>(candidateStatus);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.CandidateId, Is.EqualTo(candidateStatus.CandidateId));
            Assert.That(result.CandidateStatusId, Is.EqualTo(candidateStatus.CandidateStatusId));
            Assert.That(result.CandidateName, Is.EqualTo(candidateStatus.Candidate.Name));
            Assert.That(result.PositionTitle, Is.EqualTo(candidateStatus.Position.Title));
            Assert.That(result.PositionId, Is.EqualTo(candidateStatus.Position.PositionId));
            Assert.That(result.Status, Is.EqualTo(candidateStatus.Status));
            Assert.That(result.ContactInfo.Count, Is.EqualTo(candidate.ContactInfo.Count));

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
