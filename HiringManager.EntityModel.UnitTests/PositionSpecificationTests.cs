using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using HiringManager.EntityModel.Specifications;
using NUnit.Framework;

namespace HiringManager.EntityModel.UnitTests
{
    [TestFixture]
    public class PositionSpecificationTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
        }

        public PositionSpecification Specification { get; set; }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Specification = new PositionSpecification();
            this.AllPositions = Builder<Position>
                .CreateListOfSize(100)
                .Build()
                ;
        }

        public IList<Position> AllPositions { get; set; }

        [Test]
        public void EmptyReturnsAll()
        {
            // Arrange

            // Act
            var results = this.AllPositions
                .AsQueryable()
                .Where(this.Specification.IsSatisfied())
                .ToList()
                ;

            // Assert
            Assert.That(results.Count, Is.EqualTo(this.AllPositions.Count));
        }

        [Test]
        public void Statuses()
        {
            // Arrange
            this.Specification.Statuses = new[] {"Status2", "Status4", "Status6"};


            // Act
            var results = this.AllPositions
                .AsQueryable()
                .Where(this.Specification.IsSatisfied())
                .OrderBy(row => row.Status)
                .ToList()
                ;

            // Assert
            Assert.That(results.Count, Is.EqualTo(3));
            Assert.That(results[0].Status, Is.EqualTo("Status2"));
            Assert.That(results[1].Status, Is.EqualTo("Status4"));
            Assert.That(results[2].Status, Is.EqualTo("Status6"));

        }

        [Test]
        public void ManagerIds()
        {
            // Arrange
            this.Specification.ManagerIds = new[] {7, 8, 9};

            // Act
            var results = this.AllPositions
                .AsQueryable()
                .Where(this.Specification.IsSatisfied())
                .OrderBy(row => row.Status)
                .ToList()
                ;

            // Assert
            Assert.That(results.Count, Is.EqualTo(3));
            Assert.That(results[0].CreatedById, Is.EqualTo(7));
            Assert.That(results[1].CreatedById, Is.EqualTo(8));
            Assert.That(results[2].CreatedById, Is.EqualTo(9));


        }
    }
}
