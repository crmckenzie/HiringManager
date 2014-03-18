using HiringManager.DomainServices;
using HiringManager.EntityModel.Specifications;
using HiringManager.Mappers.Domain;
using NUnit.Framework;

namespace HiringManager.Mappers.UnitTests.Domain
{
    [TestFixture]
    public class QueryPositionSummariesRequestMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Mapper = new QueryPositionSummariesRequestMapper();
        }

        public QueryPositionSummariesRequestMapper Mapper { get; set; }

        [Test]
        public void MapNull()
        {
            // Arrange

            // Act
            var specification = this.Mapper.Map(null);

            // Assert
            Assert.That(specification, Is.Not.Null);
        }

        [Test]
        public void MapEmpty()
        {
            // Arrange

            // Act
            var specification = this.Mapper.Map(new QueryPositionSummariesRequest()) as PositionSpecification;

            // Assert
            Assert.That(specification, Is.Not.Null);
            Assert.That(specification.Statuses, Is.Null.Or.Empty);
        }

        [Test]
        public void Statuses()
        {
            // Arrange
            var request = new QueryPositionSummariesRequest()
                          {
                              Statuses = new[]{"Open"}
                          };

            // Act
            var specification = this.Mapper.Map(request) as PositionSpecification;

            // Assert
            Assert.That(specification, Is.Not.Null);
            Assert.That(specification.Statuses, Is.EquivalentTo(request.Statuses));
        }

        [Test]
        public void ManagerIds()
        {
            // Arrange
            var request = new QueryPositionSummariesRequest()
            {
                ManagerIds = new []{7, 8, 9}
            };

            // Act
            var specification = this.Mapper.Map(request) as PositionSpecification;

            // Assert
            Assert.That(specification, Is.Not.Null);
            Assert.That(specification.ManagerIds, Is.EquivalentTo(request.ManagerIds));
        }

    }
}
