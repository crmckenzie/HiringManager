using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.Domain.Specifications;
using NUnit.Framework;

namespace HiringManager.Domain.UnitTests
{
    [TestFixture]
    public class PositionSpecificationTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
            this.Specification = new PositionSpecification();
        }

        public PositionSpecification Specification { get; set; }

        [SetUp]
        public void BeforeEachTestRuns()
        {
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
        public void Status()
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
    }
}
