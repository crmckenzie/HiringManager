using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.Domain;
using HiringManager.DomainServices;
using HiringManager.Web.Mappers.Positions;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.Mappers
{
    [TestFixture]
    public class PositionCandidatesViewModelMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Mapper = new PositionCandidatesViewModelMapper();
        }

        public PositionCandidatesViewModelMapper Mapper { get; set; }

        [Test]
        public void Map()
        {
            // Arrange
            var candidates = Builder<CandidateStatusDetails>
                .CreateListOfSize(10)
                .All()
                .Do(row => row.ContactInfo = Builder<ContactInfoDetails>.CreateListOfSize(3).Build())
                .Build()
                ;
            var positionDetails = Builder<PositionDetails>
                .CreateNew()
                .Do(row => row.Candidates = candidates)
                .Build()
                ;

            // Act
            var viewModel = this.Mapper.Map(positionDetails);

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel.PositionId, Is.EqualTo(positionDetails.PositionId));
            Assert.That(viewModel.Title, Is.EqualTo(positionDetails.Title));
            Assert.That(viewModel.Candidates, Has.Count.EqualTo(positionDetails.Candidates.Count));

            for (var i = 0; i < 10; i++)
            {
                var expected = candidates[i];
                var actual = viewModel.Candidates[i];

                Assert.That(actual.CandidateName, Is.EqualTo(expected.CandidateName));
                Assert.That(actual.CandidateStatusId, Is.EqualTo(expected.CandidateStatusId));
                Assert.That(actual.CandidateId, Is.EqualTo(expected.CandidateId));

                Assert.That(actual.ContactInfo, Has.Count.EqualTo(expected.ContactInfo.Count));
                Assert.That(actual.Status, Is.EqualTo(expected.Status));

                for (var j = 0; j < 3; j++)
                {
                    var expectedContactInfo = expected.ContactInfo[j];
                    var actualContactInfo = actual.ContactInfo[j];
                    Assert.That(actualContactInfo.Type, Is.EqualTo(expectedContactInfo.Type));
                    Assert.That(actualContactInfo.Value, Is.EqualTo(expectedContactInfo.Value));
                }
            }
        }

    }
}
