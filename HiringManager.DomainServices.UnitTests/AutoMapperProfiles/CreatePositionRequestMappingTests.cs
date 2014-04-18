using FizzWare.NBuilder;
using HiringManager.DomainServices.AutoMapperProfiles;
using HiringManager.EntityModel;
using NUnit.Framework;

namespace HiringManager.DomainServices.UnitTests.AutoMapperProfiles
{
    [TestFixture]
    public class CreatePositionRequestMappingTests
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
            var request = Builder<CreatePositionRequest>
                .CreateNew()
                .Build()
                ;

            // Act
            var result = AutoMapper.Mapper.Map<Position>(request);

            // Assert
            Assert.That((object) result, Is.Not.Null);
            Assert.That((object) result.Candidates, Is.Empty);
            Assert.That((object) result.CreatedById, Is.EqualTo(request.HiringManagerId));
            Assert.That((object) result.OpenDate, Is.EqualTo(request.OpenDate));
            Assert.That((object) result.Status, Is.EqualTo("Open"));
            Assert.That((object) result.Title, Is.EqualTo(request.Title));
            Assert.That(result.Openings, Has.Count.EqualTo(request.Openings));
        }

    }
}
