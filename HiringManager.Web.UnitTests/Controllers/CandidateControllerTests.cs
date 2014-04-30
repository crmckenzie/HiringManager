using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.DomainServices.Candidates;
using HiringManager.DomainServices.Sources;
using HiringManager.EntityFramework;
using HiringManager.Web.Controllers;
using HiringManager.Web.Infrastructure.AutoMapper;
using HiringManager.Web.ViewModels;
using HiringManager.Web.ViewModels.Candidates;
using NSubstitute;
using NUnit.Framework;
using Simple.Validation;

namespace HiringManager.Web.UnitTests.Controllers
{
    [TestFixture]
    public class CandidateControllerTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
            AutoMapperConfiguration.Configure();
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.CandidateService = Substitute.For<ICandidateService>();
            this.SourceService = Substitute.For<ISourceService>();
            this.DbContext = Substitute.For<HiringManagerDbContext>();
            this.CandidateController = new CandidateController(this.CandidateService, this.SourceService, this.DbContext);
        }

        public ISourceService SourceService { get; set; }

        public ICandidateService CandidateService { get; set; }

        public HiringManagerDbContext DbContext { get; set; }

        public CandidateController CandidateController { get; set; }

        [Test]
        public void Create_HttpGet()
        {
            // Arrange
            var sources = Builder<SourceSummary>
                .CreateListOfSize(3)
                .Build()
                ;

            this.SourceService
                .Query(Arg.Any<QuerySourcesRequest>())
                .Returns(new QueryResponse<SourceSummary>() { Data = sources });

            // Act
            var actionResult = this.CandidateController.Create();

            // Assert
            Assert.That(actionResult, Is.InstanceOf<ViewResult>());
            var view = actionResult as ViewResult;
            Assert.That(view.Model, Is.InstanceOf<EditCandidateViewModel>());
            var model = view.Model as EditCandidateViewModel;

            Assert.That(model.Sources.DataTextField, Is.EqualTo("Name"));
            Assert.That(model.Sources.DataValueField, Is.EqualTo("SourceId"));
            Assert.That(model.Sources.Items, Is.EqualTo(sources));
        }

        [Test]
        public void Create_HttpPost()
        {
            // Arrange
            var viewModel = Builder<EditCandidateViewModel>
                .CreateNew()
                .Do(row => row.CandidateId = null)
                .Do(row => row.ContactInfo = Builder<ContactInfoViewModel>.CreateListOfSize(3).Build().ToArray())
                .Build()
                ;

            var response = new ValidatedResponse();

            this.CandidateService.Save(Arg.Any<SaveCandidateRequest>()).Returns(response);

            // Act
            var actionResult = this.CandidateController.Create(viewModel);

            // Assert
            this.CandidateService.Received().Save(Arg.Is(MatchesViewModel(viewModel)))
                ;

            Assert.That(actionResult, Is.InstanceOf<RedirectToRouteResult>());
            var redirect = actionResult as RedirectToRouteResult;
            Assert.That(redirect.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Create_ModelStateIsNotValid_ShouldNotSave()
        {
            // Arrange
            var sources = Builder<SourceSummary>
                .CreateListOfSize(3)
                .Build()
                ;

            this.SourceService
                .Query(Arg.Any<QuerySourcesRequest>())
                .Returns(new QueryResponse<SourceSummary>() { Data = sources });


            var viewModel = Builder<EditCandidateViewModel>
                .CreateNew()
                .Do(row => row.CandidateId = null)
                .Do(row => row.ContactInfo = Builder<ContactInfoViewModel>.CreateListOfSize(3).Build().ToArray())
                .Build()
                ;

            this.CandidateController.ModelState.AddModelError("Some Property", "Some Message");

            this.CandidateService.Save(Arg.Any<SaveCandidateRequest>()).Returns(new ValidatedResponse());

            // Act
            var actionResult = this.CandidateController.Create(viewModel);

            // Assert
            this.CandidateService.DidNotReceive().Save(Arg.Any<SaveCandidateRequest>());

            Assert.That(actionResult, Is.InstanceOf<ViewResult>());
            var view = actionResult as ViewResult;
            Assert.That(view.Model, Is.SameAs(viewModel));

            var model = view.Model as EditCandidateViewModel;
            Assert.That(model.Sources.DataTextField, Is.EqualTo("Name"));
            Assert.That(model.Sources.DataValueField, Is.EqualTo("SourceId"));
            Assert.That(model.Sources.Items, Is.EqualTo(sources));
            Assert.That(model.Sources.SelectedValue, Is.EqualTo(viewModel.SourceId.Value));
        }

        [Test]
        public void Create_ValidationErrors_ShouldNotRedirect()
        {
            // Arrange
            var sources = Builder<SourceSummary>
                .CreateListOfSize(3)
                .Build()
                ;

            this.SourceService
                .Query(Arg.Any<QuerySourcesRequest>())
                .Returns(new QueryResponse<SourceSummary>() { Data = sources });

            var viewModel = Builder<EditCandidateViewModel>
                .CreateNew()
                .Do(row => row.CandidateId = null)
                .Do(row => row.ContactInfo = Builder<ContactInfoViewModel>.CreateListOfSize(3).Build().ToArray())
                .Build()
                ;

            var validationResult = Builder<ValidationResult>.CreateNew().Build();
            var response = new ValidatedResponse()
                           {
                               ValidationResults = new[]
                                                   {
                                                       validationResult, 
                                                   },
                           };

            this.CandidateService.Save(Arg.Any<SaveCandidateRequest>()).Returns(response);

            // Act
            var actionResult = this.CandidateController.Create(viewModel);

            // Assert
            this.CandidateService.Received().Save(Arg.Is(MatchesViewModel(viewModel)));

            // Assert
            Assert.That(actionResult, Is.InstanceOf<ViewResult>());
            var view = actionResult as ViewResult;
            Assert.That(view.Model, Is.SameAs(viewModel));

            Assert.That(this.CandidateController.ModelState.ContainsKey(validationResult.PropertyName));
            Assert.That(this.CandidateController.ModelState[validationResult.PropertyName].Errors.Any(row => row.ErrorMessage == validationResult.Message));

            var model = view.Model as EditCandidateViewModel;
            Assert.That(model.Sources.DataTextField, Is.EqualTo("Name"));
            Assert.That(model.Sources.DataValueField, Is.EqualTo("SourceId"));
            Assert.That(model.Sources.Items, Is.EqualTo(sources));
            Assert.That(model.Sources.SelectedValue, Is.EqualTo(viewModel.SourceId.Value));
        }

        [Test]
        public void Edit_HttpGet()
        {
            // Arrange
            var sources = Builder<SourceSummary>
                .CreateListOfSize(3)
                .Build()
                ;

            this.SourceService
                .Query(Arg.Any<QuerySourcesRequest>())
                .Returns(new QueryResponse<SourceSummary>() { Data = sources });

            var candidateDetails = Builder<CandidateDetails>
                .CreateNew()
                .Do(row => row.ContactInfo = Builder<ContactInfoDetails>.CreateListOfSize(3).Build().ToArray())
                .Build()
                ;

            this.CandidateService.Get(1001).Returns(candidateDetails);

            // Act
            var actionResult = this.CandidateController.Edit(1001);

            // Assert
            Assert.That(actionResult, Is.InstanceOf<ViewResult>());
            var view = actionResult as ViewResult;

            Assert.That(view.Model, Is.InstanceOf<EditCandidateViewModel>());
            var model = view.Model as EditCandidateViewModel;

            Assert.That(model.Sources.DataTextField, Is.EqualTo("Name"));
            Assert.That(model.Sources.DataValueField, Is.EqualTo("SourceId"));
            Assert.That(model.Sources.Items, Is.EqualTo(sources));
            Assert.That(model.Sources.SelectedValue, Is.EqualTo(candidateDetails.SourceId.Value));
        }


        [Test]
        public void Edit_HttpPost()
        {
            // Arrange
            var viewModel = Builder<EditCandidateViewModel>
                .CreateNew()
                .Do(row => row.CandidateId = null)
                .Do(row => row.ContactInfo = Builder<ContactInfoViewModel>.CreateListOfSize(3).Build().ToArray())
                .Build()
                ;

            var response = new ValidatedResponse();

            this.CandidateService.Save(Arg.Any<SaveCandidateRequest>()).Returns(response);

            // Act
            var actionResult = this.CandidateController.Edit(viewModel);

            // Assert
            this.CandidateService.Received().Save(Arg.Is(MatchesViewModel(viewModel)))
                ;

            Assert.That(actionResult, Is.InstanceOf<RedirectToRouteResult>());
            var redirect = actionResult as RedirectToRouteResult;
            Assert.That(redirect.RouteValues["action"], Is.EqualTo("Index"));
        }

        [Test]
        public void Edit_ModelStateIsNotValid_ShouldNotSave()
        {
            // Arrange
            var sources = Builder<SourceSummary>
                .CreateListOfSize(3)
                .Build()
                ;

            this.SourceService
                .Query(Arg.Any<QuerySourcesRequest>())
                .Returns(new QueryResponse<SourceSummary>() { Data = sources });


            var viewModel = Builder<EditCandidateViewModel>
                .CreateNew()
                .Do(row => row.CandidateId = null)
                .Do(row => row.ContactInfo = Builder<ContactInfoViewModel>.CreateListOfSize(3).Build().ToArray())
                .Build()
                ;

            this.CandidateController.ModelState.AddModelError("Some Property", "Some Message");

            this.CandidateService.Save(Arg.Any<SaveCandidateRequest>()).Returns(new ValidatedResponse());

            // Act
            var actionResult = this.CandidateController.Edit(viewModel);

            // Assert
            this.CandidateService.DidNotReceive().Save(Arg.Any<SaveCandidateRequest>());

            Assert.That(actionResult, Is.InstanceOf<ViewResult>());
            var view = actionResult as ViewResult;
            Assert.That(view.Model, Is.SameAs(viewModel));

            var model = view.Model as EditCandidateViewModel;
            Assert.That(model.Sources.DataTextField, Is.EqualTo("Name"));
            Assert.That(model.Sources.DataValueField, Is.EqualTo("SourceId"));
            Assert.That(model.Sources.Items, Is.EqualTo(sources));
            Assert.That(model.Sources.SelectedValue, Is.EqualTo(viewModel.SourceId.Value));
        }

        [Test]
        public void Edit_ValidationErrors_ShouldNotRedirect()
        {
            // Arrange
            var sources = Builder<SourceSummary>
                .CreateListOfSize(3)
                .Build()
                ;

            this.SourceService
                .Query(Arg.Any<QuerySourcesRequest>())
                .Returns(new QueryResponse<SourceSummary>() { Data = sources });

            var viewModel = Builder<EditCandidateViewModel>
                .CreateNew()
                .Do(row => row.CandidateId = null)
                .Do(row => row.ContactInfo = Builder<ContactInfoViewModel>.CreateListOfSize(3).Build().ToArray())
                .Build()
                ;

            var validationResult = Builder<ValidationResult>.CreateNew().Build();
            var response = new ValidatedResponse()
            {
                ValidationResults = new[]
                                                   {
                                                       validationResult, 
                                                   },
            };

            this.CandidateService.Save(Arg.Any<SaveCandidateRequest>()).Returns(response);

            // Act
            var actionResult = this.CandidateController.Edit(viewModel);

            // Assert
            this.CandidateService.Received().Save(Arg.Is(MatchesViewModel(viewModel)));

            // Assert
            Assert.That(actionResult, Is.InstanceOf<ViewResult>());
            var view = actionResult as ViewResult;
            Assert.That(view.Model, Is.SameAs(viewModel));

            Assert.That(this.CandidateController.ModelState.ContainsKey(validationResult.PropertyName));
            Assert.That(this.CandidateController.ModelState[validationResult.PropertyName].Errors.Any(row => row.ErrorMessage == validationResult.Message));

            var model = view.Model as EditCandidateViewModel;
            Assert.That(model.Sources.DataTextField, Is.EqualTo("Name"));
            Assert.That(model.Sources.DataValueField, Is.EqualTo("SourceId"));
            Assert.That(model.Sources.Items, Is.EqualTo(sources));
            Assert.That(model.Sources.SelectedValue, Is.EqualTo(viewModel.SourceId.Value));
        }


        private static Expression<Predicate<SaveCandidateRequest>> MatchesViewModel(EditCandidateViewModel viewModel)
        {
            return req =>
                req.CandidateId == null && req.Name == viewModel.Name && req.SourceId == viewModel.SourceId;
        }
    }
}
