using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using NUnit.Framework;

namespace HiringManager.Domain.Mappers.UnitTests
{
    [TestFixture]
    public class CreatePositionRequestMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Mapper = new CreatePositionRequestMapper();
        }

        public CreatePositionRequestMapper Mapper { get; set; }

        [Test]
        public void Map()
        {
            // Arrange
            var request = Builder<CreatePositionRequest>
                .CreateNew()
                .Build()
                ;

            // Act
            var result = this.Mapper.Map(request);

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
