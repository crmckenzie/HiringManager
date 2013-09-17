using System.Linq;
using HiringManager.DomainServices;
using HiringManager.Mappers;
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
            this.PositionService = Substitute.For<IPositionService>();
            this.FluentMapper = Substitute.For<IFluentMapper>();
            this.Controller = new PositionsController(this.PositionService, this.FluentMapper);
        }

        public IFluentMapper FluentMapper { get; set; }

        public IPositionService PositionService { get; set; }

        public PositionsController Controller { get; set; }

        [Test]
        public void Index()
        {
            // Arrange
            var queryResponse = new QueryResponse<PositionSummary>();
            this.PositionService.Query(Arg.Is<QueryPositionSummariesRequest>(arg => arg.Statuses.Contains("Open"))).Returns(queryResponse);

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
