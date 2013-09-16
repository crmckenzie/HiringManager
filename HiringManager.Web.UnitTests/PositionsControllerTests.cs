using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.WebPages;
using HiringManager.Web.ApplicationServices.Positions;
using HiringManager.Web.Controllers;
using HiringManager.Web.Models;
using HiringManager.Web.Models.Positions;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests
{
    [TestFixture]
    public class PositionsControllerTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.PositionApplicationService = Substitute.For<IPositionApplicationService>();
            this.FluentMapper = Substitute.For<IFluentMapper>();
            this.Controller = new PositionsController(this.PositionApplicationService, this.FluentMapper);
        }

        public IFluentMapper FluentMapper { get; set; }

        public IPositionApplicationService PositionApplicationService { get; set; }

        public PositionsController Controller { get; set; }

        [Test]
        public void Index()
        {
            // Arrange
            var queryResponse = new QueryResponse<PositionSummary>();
            this.PositionApplicationService.GetOpenPositions().Returns(queryResponse);

            var indexViewModel = new IndexViewModel<PositionSummaryIndexItem>();
            this.FluentMapper.Map<IndexViewModel<PositionSummaryIndexItem>>()
                .From(queryResponse)
                .Returns(indexViewModel)
                ;

            // Act
            var result = this.Controller.Index();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Model, Is.SameAs(indexViewModel));
        }

    }
}
