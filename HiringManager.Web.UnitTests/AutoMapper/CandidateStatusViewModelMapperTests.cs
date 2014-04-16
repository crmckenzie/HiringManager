using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.Web.Infrastructure.AutoMapper;
using HiringManager.Web.ViewModels.Positions;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.AutoMapper
{
    [TestFixture]
    public class CandidateStatusViewModelMapperTests
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
            var details = Builder<CandidateStatusDetails>
                .CreateNew()
                .Build()
                ;

            // Act
            var viewModel = global::AutoMapper.Mapper.Map<CandidateStatusViewModel>(details);

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel.CandidateId, Is.EqualTo(details.CandidateId));
            Assert.That(viewModel.CandidateName, Is.EqualTo(details.CandidateName));
            Assert.That(viewModel.CandidateStatusId, Is.EqualTo(details.CandidateStatusId));
            Assert.That(viewModel.PositionTitle, Is.EqualTo(details.PositionTitle));
            Assert.That(viewModel.PositionId, Is.EqualTo(details.PositionId));
            Assert.That(viewModel.Status, Is.EqualTo(details.Status));
        }

    }
}
