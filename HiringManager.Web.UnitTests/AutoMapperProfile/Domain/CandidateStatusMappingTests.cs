using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.DomainServices.AutoMapperProfiles;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.AutoMapperProfile.Domain
{
    [TestFixture]
    public class CandidateStatusMappingTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.AddProfile<DomainProfile>();
        }

        [Test]
        public void FromAddCandidateRequest_WithNewCandidate()
        {
            // Arrange
            var expected = Builder<AddCandidateRequest>
                .CreateNew()
                .Do(row => row.CandidateId = null)
                .Do(row => row.ContactInfo = Builder<ContactInfoDetails>.CreateListOfSize(2).Build())
                .Build()
                ;

            // Act
            var actual = AutoMapper.Mapper.Map<CandidateStatus>(expected);

            // Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.PositionId, Is.EqualTo(expected.PositionId));
            Assert.That(actual.CandidateStatusId, Is.Null);
            Assert.That(actual.Status, Is.EqualTo("Resume Received"));

            Assert.That(actual.CandidateId, Is.Null);
            Assert.That(actual.Candidate, Is.Not.Null);
            Assert.That(actual.Candidate.Name, Is.EqualTo(expected.CandidateName));
            Assert.That(actual.Candidate.SourceId, Is.EqualTo(expected.SourceId));

            for (var i = 0; i < expected.ContactInfo.Count; i++)
            {
                var expectedContactInfo = expected.ContactInfo[i];
                var actualContactInfo = actual.Candidate.ContactInfo[i];
                Assert.That(actualContactInfo.Type, Is.EqualTo(expectedContactInfo.Type));
                Assert.That(actualContactInfo.Value, Is.EqualTo(expectedContactInfo.Value));
            }
        }

    }
}
