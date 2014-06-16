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
            var candidate = Builder<Candidate>
                .CreateNew()
                .Build()
                ;
            candidate.ContactInfo = Builder<ContactInfo>
                .CreateListOfSize(3)
                .Build()
                ;

            candidate.Documents = Builder<Document>
                .CreateListOfSize(2)
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

            Assert.That(result.Documents.Count, Is.EqualTo(candidate.Documents.Count));
            for (var i = 0; i < candidate.Documents.Count; i++)
            {
                var expected = candidate.Documents[i];
                var actual = result.Documents[i];

                Assert.That(actual.DocumentId, Is.EqualTo(expected.DocumentId));
                Assert.That(actual.Name, Is.EqualTo(expected.DisplayName));
            }
        }

    }
}
