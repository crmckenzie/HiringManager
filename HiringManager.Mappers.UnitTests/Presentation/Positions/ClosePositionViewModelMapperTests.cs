using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.Mappers.Presentation.Positions;
using NUnit.Framework;

namespace HiringManager.Mappers.UnitTests.Presentation.Positions
{
    [TestFixture]
    public class ClosePositionViewModelMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Mapper = new ClosePositionViewModelMapper();
        }

        public ClosePositionViewModelMapper Mapper { get; set; }

        [Test]
        public void Map()
        {
            // Arrange
            var positionDetails = Builder<PositionDetails>
                .CreateNew()
                .Build()
                ;

            // Act
            var result = this.Mapper.Map(positionDetails);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.PositionId, Is.EqualTo(positionDetails.PositionId));
            Assert.That(result.PositionTitle, Is.EqualTo(positionDetails.Title));
        }
    }
}
