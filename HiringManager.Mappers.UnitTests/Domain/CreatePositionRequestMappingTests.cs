using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.EntityModel;
using HiringManager.Web.Infrastructure.AutoMapper;
using NUnit.Framework;

namespace HiringManager.Mappers.UnitTests.Domain
{
    [TestFixture]
    public class CreatePositionRequestMappingTests
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
            var request = Builder<CreatePositionRequest>
                .CreateNew()
                .Build()
                ;

            // Act
            var result = AutoMapper.Mapper.Map<Position>(request);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Candidates, Is.Empty);
            Assert.That(result.CreatedById, Is.EqualTo(request.HiringManagerId));
            Assert.That(result.FilledBy, Is.Null);
            Assert.That(result.OpenDate, Is.EqualTo(request.OpenDate));
            Assert.That(result.Status, Is.EqualTo("Open"));
            Assert.That(result.Title, Is.EqualTo(request.Title));
        }

    }
}
