using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HiringManager.DomainServices;
using HiringManager.Mappers;
using HiringManager.Web.Controllers;
using HiringManager.Web.Models;
using HiringManager.Web.Models.Positions;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace HiringManager.Web.UnitTests.Controllers
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
            this.Clock = Substitute.For<IClock>();

            this.PositionService = Substitute.For<IPositionService>();
            this.FluentMapper = Substitute.For<IFluentMapper>();
            this.PositionsController = new PositionsController(this.PositionService, this.FluentMapper, this.Clock);
        }

        public IClock Clock { get; set; }

        public IFluentMapper FluentMapper { get; set; }

        public IPositionService PositionService { get; set; }

        public PositionsController PositionsController { get; set; }

        [Test]
        public void Index()
        {
            // Arrange
            var response = new QueryResponse<PositionSummary>();
            this.PositionService.Query(Arg.Is<QueryPositionSummariesRequest>(arg => arg.Statuses.Contains("Open")))
                .Returns(response)
                ;

            var indexViewModel = new IndexViewModel<PositionSummaryIndexItem>();
            this.FluentMapper
                .Map<IndexViewModel<PositionSummaryIndexItem>>()
                .From(response)
                .Returns(indexViewModel)
                ;

            // Act
            var viewResult = this.PositionsController.Index();

            // Assert
            Assert.That(viewResult.Model, Is.SameAs(indexViewModel));
        }

        [Test]
        public void Create_HttpGet()
        {
            // Arrange
            this.Clock.Today.Returns(DateTime.Today);

            // Act
            var result = this.PositionsController.Create();

            // Assert
            var viewModel = result.Model as CreatePositionViewModel;
            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel.OpenDate, Is.EqualTo(this.Clock.Today));
        }

        [Test]
        public void Create_HttpPost()
        {
            // Arrange
            var viewModel = new CreatePositionViewModel();
            var request = new CreatePositionRequest();

            this.FluentMapper
                .Map<CreatePositionRequest>()
                .From(viewModel)
                .Returns(request)
                ;

            // Act
            var result = this.PositionsController.Create(viewModel);

            // Assert
            this.PositionService
                .Received()
                .CreatePosition(request)
                ;
        }

        [Test]
        public void Candidates()
        {
            // Arrange
            var positionDetails = new PositionDetails();
            var viewModel = new PositionCandidatesViewModel();

            this.PositionService.Details(1).Returns(positionDetails);

            this.FluentMapper
                .Map<PositionCandidatesViewModel>()
                .From(positionDetails)
                .Returns(viewModel)
                ;

            this.PositionService.Details(1).Returns(positionDetails);

            // Act
            var viewResult = this.PositionsController.Candidates(1);

            // Assert
            Assert.That(viewResult.Model, Is.SameAs(viewModel));
        }
    }
}
