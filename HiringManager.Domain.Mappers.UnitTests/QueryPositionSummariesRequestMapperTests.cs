using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HiringManager.Domain.Specifications;
using HiringManager.DomainServices;
using NUnit.Framework;

namespace HiringManager.Domain.Mappers.UnitTests
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
    }
}
