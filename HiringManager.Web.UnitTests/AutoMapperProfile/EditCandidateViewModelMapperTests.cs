using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.Web.Infrastructure.AutoMapper;
using HiringManager.Web.ViewModels;
using HiringManager.Web.ViewModels.Candidates;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.AutoMapperProfile
{
    [TestFixture]
    public class EditCandidateViewModelMapperTests
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
            var expected = Builder<EditCandidateViewModel>
                .CreateNew()
                .Do(row => row.ContactInfo = Builder<ContactInfoViewModel>.CreateListOfSize(3).Build().ToArray())
                .Build()
                ;

            // Act
            var actual = Mapper.Map<SaveCandidateRequest>(expected);

            // Assert
            Assert.That(actual.CandidateId, Is.EqualTo(expected.CandidateId));
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.SourceId, Is.EqualTo(expected.SourceId));

            for (int i = 0; i < expected.ContactInfo.Length; i++)
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