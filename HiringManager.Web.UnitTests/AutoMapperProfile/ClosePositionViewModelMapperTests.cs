using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.Web.Infrastructure.AutoMapper;
using HiringManager.Web.ViewModels.Positions;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.AutoMapperProfile
{
    [TestFixture]
    public class ClosePositionViewModelMapperTests
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
            var positionDetails = Builder<PositionDetails>
                .CreateNew()
                .Build()
                ;

            // Act
            var result = global::AutoMapper.Mapper.Map<ClosePositionViewModel>(positionDetails);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PositionId, Is.EqualTo(positionDetails.PositionId));
            Assert.That(result.PositionTitle, Is.EqualTo(positionDetails.Title));
        }
    }
}
