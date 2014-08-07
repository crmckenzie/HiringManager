using System;
using System.Linq;
using System.Web.Mvc;
using FizzWare.NBuilder;
using HiringManager.DomainServices.Authentication;
using HiringManager.DomainServices.Candidates;
using HiringManager.DomainServices.Positions;
using HiringManager.DomainServices.Sources;
using HiringManager.DomainServices.Validators.UnitTests;
using HiringManager.Web.Controllers;
using HiringManager.Web.Infrastructure.AutoMapper;
using HiringManager.Web.ViewModels;
using HiringManager.Web.ViewModels.Positions;
using NSubstitute;
using NUnit.Framework;
using Simple.Validation;
using TestHelpers;

namespace HiringManager.Web.UnitTests.Controllers
{
    [TestFixture]
    public class PositionsControllerTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
            AutoMapperConfiguration.Configure();
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Clock = Substitute.For<IClock>();

            this.SourceService = Substitute.For<ISourceService>();
            this.PositionService = Substitute.For<IPositionService>();
            this.CandidateService = Substitute.For<ICandidateService>();
            this.UserSession = Substitute.For<IUserSession>();
            this.Controller = new PositionsController(this.PositionService, this.SourceService, this.CandidateService, this.UserSession,
                this.Clock);

            StubSources();

            StubCandidates();
        }

        private void StubCandidates()
        {
            this.Candidates = Builder<CandidateSummary>
                .CreateListOfSize(3)
                .Build()
                .ToArray()
                ;


            this.CandidateService.Query(null).Returns(new QueryResponse<CandidateSummary>
                                                      {
                                                          Data = this.Candidates
                                                      });
        }

        private void StubSources()
        {
            this.Sources = Builder<SourceSummary>
                .CreateListOfSize(3)
                .Build()
                .ToArray()
                ;
            this.SourceService.Query(null).Returns(new QueryResponse<SourceSummary>
                                                   {
                                                       Data = this.Sources,
                                                   });
        }

        public CandidateSummary[] Candidates { get; set; }

        public SourceSummary[] Sources { get; set; }

        public ICandidateService CandidateService { get; set; }

        public ISourceService SourceService { get; set; }

        public IUserSession UserSession { get; set; }

        public IClock Clock { get; set; }

        public IPositionService PositionService { get; set; }

        public PositionsController Controller { get; set; }

        [Test]
        public void Index()
        {
            // Arrange
            this.UserSession.ManagerId.Returns(12345);

            var response = new QueryResponse<PositionSummary>();

            Func<QueryPositionSummariesRequest, bool> constraint = arg =>
                arg.Statuses == null && arg.ManagerIds.Contains(12345);


            this.PositionService.Query(Arg.Is<QueryPositionSummariesRequest>(arg => constraint(arg)))
                .Returns(response)
                ;

            // Act
            var viewResult = this.Controller.Index("");

            // Assert
            Assert.That(viewResult.Model, Is.InstanceOf<IndexViewModel<PositionSummaryIndexItem>>());
        }

        [Test]
        public void Index_WithStatus()
        {
            // Arrange
            this.UserSession.ManagerId.Returns(12345);
            var response = new QueryResponse<PositionSummary>();
            this.PositionService.Query(
                Arg.Is<QueryPositionSummariesRequest>(
                    arg => arg.Statuses.Contains("Open") && arg.ManagerIds.Contains(12345)))
                .Returns(response)
                ;

            // Act
            var viewResult = this.Controller.Index("Open");

            // Assert
            Assert.That(viewResult.Model, Is.InstanceOf<IndexViewModel<PositionSummaryIndexItem>>());
        }

        [Test]
        public void Create_HttpGet()
        {
            // Arrange
            this.Clock.Today.Returns(DateTime.Today);

            // Act
            var result = this.Controller.Create();

            // Assert
            var viewModel = result.Model as CreatePositionViewModel;
            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel.OpenDate, Is.EqualTo(this.Clock.Today));
            Assert.That(viewModel.Openings, Is.EqualTo(1));
        }

        [Test]
        public void Create_HttpPost()
        {
            // Arrange
            var viewModel = new CreatePositionViewModel();
            var response = new CreatePositionResponse();

            this.PositionService.CreatePosition(Arg.Any<CreatePositionRequest>()).Returns(response);

            // Act
            var result = this.Controller.Create(viewModel);

            // Assert
            this.PositionService
                .Received()
                .CreatePosition(Arg.Any<CreatePositionRequest>())
                ;

            var redirect = result as RedirectToRouteResult;

            Assert.That(redirect, Is.Not.Null);
            Assert.That(redirect.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(redirect.RouteValues["controller"], Is.EqualTo("Positions"));
            Assert.That(redirect.RouteValues["status"], Is.EqualTo("Open"));
        }

        [Test]
        public void Create_HttpPost_ModelStateInValid()
        {
            // Arrange
            var viewModel = new CreatePositionViewModel();
            var request = new CreatePositionRequest();

            this.Controller.ModelState.AddModelError("Some DocumentId", "Some Message");

            // Act
            var result = this.Controller.Create(viewModel);

            // Assert
            this.PositionService
                .DidNotReceive()
                .CreatePosition(Arg.Any<CreatePositionRequest>())
                ;

            var viewResult = result as ViewResult;

            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.Model, Is.SameAs(viewModel));
        }

        [Test]
        public void Create_HttpPost_ValidationErrors()
        {
            // Arrange
            var viewModel = new CreatePositionViewModel();

            var validationResult = new ValidationResult
                                   {
                                       PropertyName = "Some DocumentId",
                                       Message = "Some Message"
                                   };
            this.PositionService.CreatePosition(Arg.Any<CreatePositionRequest>())
                .Returns(new CreatePositionResponse
                         {
                             ValidationResults = new[]
                                                 {
                                                     validationResult
                                                 }
                         });

            // Act
            var result = this.Controller.Create(viewModel);

            // Assert
            this.Controller.ModelState.ContainsValidationResult(validationResult);

            var viewResult = result as ViewResult;

            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.Model, Is.SameAs(viewModel));
        }

        [Test]
        public void Candidates_HttpGet()
        {
            // Arrange
            var positionDetails = new PositionDetails();

            this.PositionService.Details(1).Returns(positionDetails);

            // Act
            var viewResult = this.Controller.Candidates(1);

            // Assert
            Assert.That(viewResult.Model, Is.InstanceOf<PositionCandidatesViewModel>());
        }

        [Test]
        public void NewCandidate_HttpGet()
        {
            // Arrange

            // Act
            var viewResult = this.Controller.NewCandidate(1);

            // Assert
            var viewModel = viewResult.Model as NewCandidateViewModel;
            Assert.That(viewModel.PositionId, Is.EqualTo(1));
            Assert.That(viewModel.SourceId, Is.Null);
            Assert.That(viewModel.Sources, Is.Not.Null);
            Assert.That(viewModel.Sources.Items, Is.EqualTo(this.Sources));
        }

        [Test]
        public void NewCandidate_HttpPost()
        {
            // Arrange
            var viewModel = new NewCandidateViewModel();

            var response = Builder<NewCandidateResponse>
                .CreateNew()
                .Build();

            this.PositionService.AddNewCandidate(Arg.Any<NewCandidateRequest>()).Returns(response);

            // Act
            var actionResult = this.Controller.NewCandidate(viewModel);

            // Assert
            var redirectToAction = actionResult as RedirectToRouteResult;
            Assert.That(redirectToAction, Is.Not.Null);

            Assert.That(redirectToAction.RouteName, Is.EqualTo(""));

            Assert.That(redirectToAction.RouteValues.ContainsKey("action"), Is.True);
            Assert.That(redirectToAction.RouteValues["action"], Is.EqualTo("Candidates"));

            Assert.That(redirectToAction.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(redirectToAction.RouteValues["id"], Is.EqualTo(response.PositionId));
        }

        [Test]
        public void NewCandidate_HttpPost_WithValidationErrors()
        {
            var model = Builder<NewCandidateViewModel>
                .CreateNew()
                .Build()
                ;
            var validationResults = Builder<ValidationResult>
                .CreateListOfSize(3)
                .Build()
                ;

            var response = new NewCandidateResponse
                               {
                                   ValidationResults = validationResults
                               };
            this.PositionService.AddNewCandidate(Arg.Any<NewCandidateRequest>())
                .Returns(response);

            // Act
            var result = this.Controller.NewCandidate(model) as ViewResult;

            // Assert
            Assert.That(result.Model, Is.SameAs(model));

            var actualResults = this.Controller.GetModelStateValidationResults();

            foreach (var expectedResult in validationResults)
            {
                actualResults.AssertInvalidFor(expectedResult);
            }

            var viewModel = result.Model as NewCandidateViewModel;
            Assert.That(viewModel.Sources.Items, Is.EqualTo(this.Sources));
        }


        [Test]
        public void AddCandidate_HttpGet()
        {
            // Arrange

            // Act
            var viewResult = this.Controller.AddCandidate(1);

            // Assert
            var viewModel = viewResult.Model as AddCandidateViewModel;
            Assert.That(viewModel.PositionId, Is.EqualTo(1));
            Assert.That(viewModel.Candidates.Items, Is.EqualTo(this.Candidates));
        }

        [Test]
        public void AddCandidate_HttpPost()
        {
            // Arrange
            var viewModel = new AddCandidateViewModel();

            var response = Builder<AddCandidateResponse>
                .CreateNew()
                .Build();

            this.PositionService.AddCandidate(Arg.Any<AddCandidateRequest>()).Returns(response);

            // Act
            var actionResult = this.Controller.AddCandidate(viewModel);

            // Assert
            var redirectToAction = actionResult as RedirectToRouteResult;
            Assert.That(redirectToAction, Is.Not.Null);

            Assert.That(redirectToAction.RouteName, Is.EqualTo(""));

            Assert.That(redirectToAction.RouteValues.ContainsKey("action"), Is.True);
            Assert.That(redirectToAction.RouteValues["action"], Is.EqualTo("Candidates"));

            Assert.That(redirectToAction.RouteValues.ContainsKey("id"), Is.True);
            Assert.That(redirectToAction.RouteValues["id"], Is.EqualTo(response.PositionId));
        }

        [Test]
        public void AddCandidate_HttpPost_WithValidationErrors()
        {
            var model = Builder<AddCandidateViewModel>
                .CreateNew()
                .Build()
                ;
            var validationResults = Builder<ValidationResult>
                .CreateListOfSize(3)
                .Build()
                ;

            var response = new AddCandidateResponse
            {
                ValidationResults = validationResults
            };
            this.PositionService.AddCandidate(Arg.Any<AddCandidateRequest>())
                .Returns(response);

            // Act
            var result = this.Controller.AddCandidate(model) as ViewResult;

            // Assert
            Assert.That(result.Model, Is.SameAs(model));

            var actualResults = this.Controller.GetModelStateValidationResults();

            foreach (var expectedResult in validationResults)
            {
                actualResults.AssertInvalidFor(expectedResult);
            }

            var viewModel = result.Model as AddCandidateViewModel;
            Assert.That(viewModel.Candidates.Items, Is.EqualTo(this.Candidates));
        }



        [Test]
        public void PassOnCandidate_HttpGet()
        {
            // Arrange
            var candidateStatusDetails = new CandidateStatusDetails();
            this.PositionService.GetCandidateStatusDetails(1)
                .Returns(candidateStatusDetails);

            // Act
            var viewResult = this.Controller.Pass(1);

            // Assert
            Assert.That(viewResult.Model, Is.InstanceOf<CandidateStatusViewModel>());
        }

        [Test]
        public void PassOnCandidate_HttpPost()
        {
            // Arrange
            var model = Builder<CandidateStatusViewModel>
                .CreateNew()
                .Build()
                ;

            // Act
            var result = this.Controller.Pass(model) as RedirectToRouteResult;

            // Assert
            Assert.That(result.RouteName, Is.EqualTo(""));

            Assert.That(result.RouteValues.ContainsKey("action"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Candidates"));

            Assert.That(result.RouteValues.ContainsKey("id"));
            Assert.That(result.RouteValues["id"], Is.EqualTo(model.PositionId));

            this.PositionService
                .Received()
                .SetCandidateStatus(model.CandidateStatusId, "Passed");
        }

        [Test]
        public void Status_HttpGet()
        {
            // Arrange
            var candidateStatusDetails = new CandidateStatusDetails();
            this.PositionService.GetCandidateStatusDetails(1)
                .Returns(candidateStatusDetails);

            // Act
            var viewResult = this.Controller.Status(1);

            // Assert
            Assert.That(viewResult.Model, Is.InstanceOf<CandidateStatusViewModel>());
        }

        [Test]
        public void Status_HttpPost()
        {
            // Arrange
            var model = Builder<CandidateStatusViewModel>
                .CreateNew()
                .Build()
                ;

            // Act
            var result = this.Controller.Status(model) as RedirectToRouteResult;

            // Assert
            Assert.That(result.RouteName, Is.EqualTo(""));

            Assert.That(result.RouteValues.ContainsKey("action"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Candidates"));

            Assert.That(result.RouteValues.ContainsKey("id"));
            Assert.That(result.RouteValues["id"], Is.EqualTo(model.PositionId));

            this.PositionService
                .Received()
                .SetCandidateStatus(model.CandidateStatusId, model.Status);
        }

        [Test]
        public void Hire_HttpGet()
        {
            // Arrange
            var candidateStatusDetails = new CandidateStatusDetails();
            this.PositionService.GetCandidateStatusDetails(1)
                .Returns(candidateStatusDetails);

            // Act
            var viewResult = this.Controller.Hire(1);

            // Assert
            Assert.That(viewResult.Model, Is.InstanceOf<CandidateStatusViewModel>());
        }

        [Test]
        public void Hire_HttpPost()
        {
            // Arrange
            var model = Builder<CandidateStatusViewModel>
                .CreateNew()
                .Build()
                ;

            this.PositionService.Hire(model.CandidateStatusId).Returns(new CandidateStatusResponse());

            // Act
            var result = this.Controller.Hire(model) as RedirectToRouteResult;

            // Assert
            Assert.That(result.RouteName, Is.EqualTo(""));

            Assert.That(result.RouteValues.ContainsKey("action"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Candidates"));

            Assert.That(result.RouteValues.ContainsKey("id"));
            Assert.That(result.RouteValues["id"], Is.EqualTo(model.PositionId));

            this.PositionService
                .Received()
                .Hire(model.CandidateStatusId);
        }

        [Test]
        public void Hire_HttpPost_WithValidationErrors()
        {
            var model = Builder<CandidateStatusViewModel>
                .CreateNew()
                .Build()
                ;
            var validationResults = Builder<ValidationResult>
                .CreateListOfSize(3)
                .Build()
                ;

            var hireResponse = new CandidateStatusResponse
                               {
                                   ValidationResults = validationResults
                               };
            this.PositionService.Hire(model.CandidateStatusId).Returns(hireResponse);

            // Act
            var result = this.Controller.Hire(model) as ViewResult;

            // Assert
            Assert.That(result.Model, Is.SameAs(model));

            var actualResults = this.Controller.GetModelStateValidationResults();

            foreach (var expectedResult in validationResults)
            {
                actualResults.AssertInvalidFor(expectedResult);
            }
        }

        [Test]
        public void Hire_HttpPost_WithModelStateErrors()
        {
            this.Controller.ModelState.AddModelError("", "Test Error Message");

            var model = Builder<CandidateStatusViewModel>
                .CreateNew()
                .Build()
                ;

            // Act
            var result = this.Controller.Hire(model) as ViewResult;

            // Assert
            this.PositionService.DidNotReceive().Hire(model.CandidateStatusId);
            Assert.That(result.Model, Is.SameAs(model));
        }

        [Test]
        public void Close_HttpGet()
        {
            // Arrange
            var positionDetails = new PositionDetails();
            this.PositionService.Details(1)
                .Returns(positionDetails);

            // Act
            var viewResult = this.Controller.Close(1) as ViewResult;

            // Assert
            Assert.That(viewResult.Model, Is.InstanceOf<ClosePositionViewModel>());
        }

        [Test]
        public void Close_HttpPost()
        {
            // Arrange
            var model = Builder<ClosePositionViewModel>
                .CreateNew()
                .Build()
                ;

            this.PositionService.Close(model.PositionId).Returns(new CandidateStatusResponse());

            // Act
            var result = this.Controller.Close(model) as RedirectToRouteResult;

            // Assert
            Assert.That(result.RouteName, Is.EqualTo(""));

            Assert.That(result.RouteValues.ContainsKey("action"));
            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));


            this.PositionService
                .Received()
                .Close(model.PositionId);
        }

        [Test]
        public void Close_HttpPost_WitValidationErrors()
        {
            var model = Builder<ClosePositionViewModel>
                .CreateNew()
                .Build()
                ;
            var validationResults = Builder<ValidationResult>
                .CreateListOfSize(3)
                .Build()
                ;

            var response = new CandidateStatusResponse
                           {
                               ValidationResults = validationResults
                           };
            this.PositionService.Close(model.PositionId).Returns(response);

            // Act
            var result = this.Controller.Close(model) as ViewResult;

            // Assert
            Assert.That(result.Model, Is.SameAs(model));

            var actualResults = this.Controller.GetModelStateValidationResults();

            foreach (var expectedResult in validationResults)
            {
                actualResults.AssertInvalidFor(expectedResult);
            }
        }

        [Test]
        public void Close_HttpPost_WithModelStateErrors()
        {
            this.Controller.ModelState.AddModelError("", "Test Error Message");

            var model = Builder<ClosePositionViewModel>
                .CreateNew()
                .Build()
                ;

            // Act
            var result = this.Controller.Close(model) as ViewResult;

            // Assert
            this.PositionService.DidNotReceive().Close(model.PositionId);
            Assert.That(result.Model, Is.SameAs(model));
        }
    }
}