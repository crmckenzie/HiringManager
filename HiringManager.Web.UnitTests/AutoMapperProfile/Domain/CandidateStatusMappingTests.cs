using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.DomainServices.AutoMapperProfiles;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using NSubstitute;
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
        public void FromNewCandidateRequest()
        {
            // Arrange
            var file1 = Substitute.For<HttpPostedFileBase>();
            var file2 = Substitute.For<HttpPostedFileBase>();
            file1.FileName.Returns("File 1");
            file2.FileName.Returns("File 2");
            var documents = new[]
                            {
                                file1,
                                file2,
                            }.ToDictionary(row => row.FileName, row => row.InputStream);


            var expected = Builder<NewCandidateRequest>
                .CreateNew()
                .Do(row => row.ContactInfo = Builder<ContactInfoDetails>.CreateListOfSize(2).Build())
                .Do(row => row.Documents = documents)
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

            Assert.That(actual.Candidate.ContactInfo.Count, Is.EqualTo(expected.ContactInfo.Count));
            for (var i = 0; i < expected.ContactInfo.Count; i++)
            {
                var expectedContactInfo = expected.ContactInfo[i];
                var actualContactInfo = actual.Candidate.ContactInfo[i];
                Assert.That(actualContactInfo.Type, Is.EqualTo(expectedContactInfo.Type));
                Assert.That(actualContactInfo.Value, Is.EqualTo(expectedContactInfo.Value));
            }

            Assert.That(actual.Candidate.Documents.Count, Is.EqualTo(expected.Documents.Count));
            foreach (var key in expected.Documents.Keys)
            {
                var actualDocument = actual.Candidate.Documents.SingleOrDefault(row => row.DisplayName == key);
                Assert.That(actualDocument, Is.Not.Null);
                Assert.That(actualDocument.DisplayName, Is.EqualTo(key));
                Assert.That(actualDocument.FileName, Is.Null);
            }

        }

        [Test]
        public void FromAddCandidateRequest()
        {
            // Arrange
            var expected = Builder<AddCandidateRequest>
                .CreateNew()
                .Build()
                ;

            // Act
            var actual = AutoMapper.Mapper.Map<CandidateStatus>(expected);

            // Assert
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.PositionId, Is.EqualTo(expected.PositionId));
            Assert.That(actual.CandidateStatusId, Is.Null);
            Assert.That(actual.Status, Is.EqualTo("Resume Received"));

            Assert.That(actual.CandidateId, Is.EqualTo(expected.CandidateId));
            Assert.That(actual.Candidate, Is.Null);

        }

    }
}
