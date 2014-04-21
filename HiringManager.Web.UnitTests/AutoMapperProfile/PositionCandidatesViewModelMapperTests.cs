using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.Web.Infrastructure.AutoMapper;
using HiringManager.Web.ViewModels.Positions;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.AutoMapperProfile
{
    [TestFixture]
    public class PositionCandidatesViewModelMapperTests
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
            var candidates = Builder<CandidateStatusDetails>
                .CreateListOfSize(10)
                .All()
                .Do(row => row.ContactInfo = Builder<ContactInfoDetails>.CreateListOfSize(3).Build())
                .Do(row =>
                    {
                        row.SourceId = 1001;
                        row.Source = "2341234";
                        row.CanHire = true;
                        row.CanPass = true;
                        row.CanSetStatus = true;
                    })
                .Build()
                ;
            var positionDetails = Builder<PositionDetails>
                .CreateNew()
                .Do(row => row.Candidates = candidates)
                .Do(row => row.CanAddCandidate = true)
                .Do(row => row.CanClose = true)
                .Build()
                ;

            // Act
            var viewModel = global::AutoMapper.Mapper.Map<PositionCandidatesViewModel>(positionDetails);

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel.PositionId, Is.EqualTo(positionDetails.PositionId));
            Assert.That(viewModel.Status, Is.EqualTo(positionDetails.Status));
            Assert.That(viewModel.Title, Is.EqualTo(positionDetails.Title));
            Assert.That(viewModel.CanAddCandidate, Is.EqualTo(positionDetails.CanAddCandidate), "CanAddCandidate");
            Assert.That(viewModel.Candidates, Has.Count.EqualTo(positionDetails.Candidates.Count));
            Assert.That(viewModel.Openings, Is.EqualTo(positionDetails.Openings));
            Assert.That(viewModel.OpeningsFilled, Is.EqualTo(positionDetails.OpeningsFilled));

            for (var i = 0; i < 10; i++)
            {
                var expected = candidates[i];
                var actual = viewModel.Candidates[i];

                Assert.That(actual.CandidateName, Is.EqualTo(expected.CandidateName));
                Assert.That(actual.CandidateStatusId, Is.EqualTo(expected.CandidateStatusId));
                Assert.That(actual.CandidateId, Is.EqualTo(expected.CandidateId));

                Assert.That(actual.CanHire, Is.EqualTo(expected.CanHire));
                Assert.That(actual.CanPass, Is.EqualTo(expected.CanPass));
                Assert.That(actual.CanSetStatus, Is.EqualTo(expected.CanSetStatus));

                Assert.That(actual.ContactInfo, Has.Count.EqualTo(expected.ContactInfo.Count));
                Assert.That(actual.Status, Is.EqualTo(expected.Status));
                Assert.That(actual.Source, Is.EqualTo(expected.Source));
                Assert.That(actual.SourceId, Is.EqualTo(expected.SourceId));

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
