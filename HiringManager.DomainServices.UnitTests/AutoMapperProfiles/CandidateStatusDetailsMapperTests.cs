using FizzWare.NBuilder;
using HiringManager.DomainServices.AutoMapperProfiles;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using NUnit.Framework;

namespace HiringManager.DomainServices.UnitTests.AutoMapperProfiles
{
    [TestFixture]
    public class CandidateStatusDetailsMapperTests
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
            Assert.That((object) result, Is.Not.Null);
            Assert.That((object) result.CandidateId, Is.EqualTo(candidateStatus.CandidateId));
            Assert.That((object) result.CandidateStatusId, Is.EqualTo(candidateStatus.CandidateStatusId));
            Assert.That((object) result.CandidateName, Is.EqualTo(candidateStatus.Candidate.Name));
            Assert.That((object) result.PositionTitle, Is.EqualTo(candidateStatus.Position.Title));
            Assert.That((object) result.PositionId, Is.EqualTo(candidateStatus.Position.PositionId));
            Assert.That((object) result.Status, Is.EqualTo(candidateStatus.Status));
            Assert.That((object) result.ContactInfo.Count, Is.EqualTo(candidate.ContactInfo.Count));

            for (var i = 0; i < candidate.ContactInfo.Count; i++)
            {
                var expected = candidate.ContactInfo[i];
                var actual = result.ContactInfo[i];
                Assert.That((object) actual.ContactInfoId, Is.EqualTo(expected.ContactInfoId));
                Assert.That((object) actual.Type, Is.EqualTo(expected.Type));
                Assert.That((object) actual.Value, Is.EqualTo(expected.Value));
            }
        }

    }
}
