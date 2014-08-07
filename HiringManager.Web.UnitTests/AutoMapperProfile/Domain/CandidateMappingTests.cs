using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.DomainServices.AutoMapperProfiles;
using HiringManager.DomainServices.Candidates;
using HiringManager.EntityModel;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.AutoMapperProfile.Domain
{
    [TestFixture]
    public class CandidateMappingTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            Mapper.Reset();
            Mapper.AddProfile<DomainProfile>();
        }

        [Test]
        public void ToCandidateSummaryViaProjection()
        {
            // Arrange
            var candidates = Builder<Candidate>
                .CreateListOfSize(5)
                .All()
                .Do(row => row.Source = new Source())
                .Build()
                .AsQueryable()
                ;


            // Act

            // Assert
            Assert.DoesNotThrow(() =>
                                {
                                    var results = candidates
                                        .Project().To<CandidateSummary>()
                                        .ToArray()
                                        ;
                                });
        }

        [Test]
        public void ToCandidateDetails()
        {
            // Arrange
            var expected = Builder<Candidate>
                .CreateNew()
                .Do(row => row.Source = new Source()
                                        {
                                            SourceId = 12341234,
                                            Name = Guid.NewGuid().ToString(),
                                        })
                .Do(row => row.ContactInfo = Builder<ContactInfo>.CreateListOfSize(3).Build().ToArray())
                .Build()
                ;

            // Act
            var actual = Mapper.Map<CandidateDetails>(expected);

            // Assert
            Assert.That(actual.CandidateId, Is.EqualTo(expected.CandidateId));
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.SourceId, Is.EqualTo(expected.SourceId));
            Assert.That(actual.Source, Is.EqualTo(expected.Source.Name));

            for (var i = 0; i < expected.ContactInfo.Count; i++)
            {
                var expectedContact = expected.ContactInfo[i];
                var actualContact = actual.ContactInfo[i];

                Assert.That(actualContact.ContactInfoId, Is.EqualTo(expectedContact.ContactInfoId));
                Assert.That(actualContact.Type, Is.EqualTo(expectedContact.Type));
                Assert.That(actualContact.Value, Is.EqualTo(expectedContact.Value));
            }
        }

        [Test]
        public void ToCandidateSummary()
        {
            // Arrange
            var expected = Builder<Candidate>
                .CreateNew()
                .Do(row => row.ContactInfo = Builder<ContactInfo>.CreateListOfSize(3).Build().ToArray())
                .Build()
                ;

            // Act
            var actual = Mapper.Map<CandidateSummary>(expected);

            // Assert
            Assert.That(actual.CandidateId, Is.EqualTo(expected.CandidateId));
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.SourceId, Is.EqualTo(expected.SourceId));
        }

        [Test]
        public void FromSaveCandidateRequest()
        {
            // Arrange
            var expected = Builder<SaveCandidateRequest>
                .CreateNew()
                .Do(row => row.ContactInfo = Builder<ContactInfoDetails>.CreateListOfSize(3).Build().ToArray())
                .Build()
                ;

            // Act
            var actual = Mapper.Map<Candidate>(expected);

            // Assert
            Assert.That(actual.CandidateId, Is.EqualTo(expected.CandidateId));
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.SourceId, Is.EqualTo(expected.SourceId));

            for (var i = 0; i < expected.ContactInfo.Length; i++)
            {
                var expectedContact = expected.ContactInfo[i];
                var actualContact = actual.ContactInfo[i];

                Assert.That(actualContact.ContactInfoId, Is.EqualTo(expectedContact.ContactInfoId));
                Assert.That(actualContact.Type, Is.EqualTo(expectedContact.Type));
                Assert.That(actualContact.Value, Is.EqualTo(expectedContact.Value));
            }
        }
    }
}