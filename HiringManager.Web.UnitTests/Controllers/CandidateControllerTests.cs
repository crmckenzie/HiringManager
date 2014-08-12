using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
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
using IntegrationTestHelpers;
using NSubstitute;
using NUnit.Framework;
using Simple.Validation;
using TestHelpers;

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
            this.UploadService = Substitute.For<IUploadService>();
            this.CandidateService = Substitute.For<ICandidateService>();
            this.SourceService = Substitute.For<ISourceService>();
            this.DbContext = Substitute.For<HiringManagerDbContext>();
            this.CandidateController = new CandidateController(this.CandidateService, this.SourceService,
                this.UploadService, this.DbContext);
        }

        public IUploadService UploadService { get; set; }

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

            this.CandidateController.ModelState.AddModelError("Some DocumentId", "Some Message");

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
            Assert.That(
                this.CandidateController.ModelState[validationResult.PropertyName].Errors.Any(
                    row => row.ErrorMessage == validationResult.Message));

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

            this.CandidateController.ModelState.AddModelError("Some DocumentId", "Some Message");

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
            Assert.That(
                this.CandidateController.ModelState[validationResult.PropertyName].Errors.Any(
                    row => row.ErrorMessage == validationResult.Message));

            var model = view.Model as EditCandidateViewModel;
            Assert.That(model.Sources.DataTextField, Is.EqualTo("Name"));
            Assert.That(model.Sources.DataValueField, Is.EqualTo("SourceId"));
            Assert.That(model.Sources.Items, Is.EqualTo(sources));
            Assert.That(model.Sources.SelectedValue, Is.EqualTo(viewModel.SourceId.Value));
        }

        [Test]
        public void Details()
        {
            // Arrange
            const int candidateId = 67;
            const string emailAddress = "candidate@candidate.com";
            const string phoneNumber = "555-123-4567";
            const int documentCount = 2;
            var details = CreateCandidateDetails(candidateId, emailAddress, phoneNumber, documentCount);


            this.CandidateService.Get(candidateId).Returns(details);

            // Act
            var result = this.CandidateController.Details(candidateId);

            // Assert
            var viewModel = result.GetViewModel<CandidateDetailsViewModel>();

            Assert.That(viewModel.CandidateId, Is.EqualTo(details.CandidateId));
            Assert.That(viewModel.Name, Is.EqualTo(details.Name));
            Assert.That(viewModel.Source, Is.EqualTo(details.Source));
            Assert.That(viewModel.SourceId, Is.EqualTo(details.SourceId));

            Assert.That(viewModel.ContactInfo, Is.EquivalentTo(details.ContactInfo)
                .Using<ContactInfoDetails, ContactInfoViewModel>(ContactInfoMatches)
                );


            Assert.That(viewModel.Documents, Is.EquivalentTo(details.Documents)
                .Using<DocumentDetails, SelectListItem>((expected, actual) =>
                    expected.DocumentId.ToString() == actual.Value && expected.Title == actual.Text));
        }

        private static bool ContactInfoMatches(ContactInfoDetails expected, ContactInfoViewModel actual)
        {
            return expected.ContactInfoId == actual.ContactInfoId && expected.Type == actual.Type &&
                   expected.Value == actual.Value;
        }

        private static CandidateDetails CreateCandidateDetails(int candidateId, string emailAddress, string phoneNumber,
            int documentCount)
        {
            var details = Builder<CandidateDetails>.CreateNew().Build();
            details.CandidateId = candidateId;
            details.ContactInfo = Builder<ContactInfoDetails>
                .CreateListOfSize(2)
                .TheFirst(1)
                .Do(row =>
                    {
                        row.Type = "Email";
                        row.Value = emailAddress;
                    })
                .TheLast(1)
                .Do(row =>
                    {
                        row.Type = "Phone";
                        row.Value = phoneNumber;
                    })
                .Build()
                .ToArray()
                ;

            details.Documents = Builder<DocumentDetails>
                .CreateListOfSize(documentCount)
                .Build()
                .ToArray()
                ;

            return details;
        }

        private static Expression<Predicate<SaveCandidateRequest>> MatchesViewModel(EditCandidateViewModel viewModel)
        {
            return req =>
                req.CandidateId == null && req.Name == viewModel.Name && req.SourceId == viewModel.SourceId;
        }

        [Test]
        public void DownloadDocument()
        {
            // Arrange
            var download = new FileDownload()
                           {
                               Stream = new MemoryStream(),
                               FileName = "filename.pdf",
                           };

            this.UploadService.Download(11234).Returns(download);

            // Act
            var response = this.CandidateController.Download(11234);

            // Assert
            Assert.That(response, Is.InstanceOf<FileStreamResult>());
            var fsr = (FileStreamResult)response;

            Assert.That(fsr.FileDownloadName, Is.EqualTo(download.FileName));
            Assert.That(fsr.ContentType, Is.EqualTo("application/pdf"));
            Assert.That(fsr.FileStream, Is.SameAs(download.Stream));
        }

        [Test]
        public void UploadDocument()
        {
            // Arrange
            var postedFile = Substitute.For<HttpPostedFileBase>();
            postedFile.InputStream.Returns(new MemoryStream());
            postedFile.FileName.Returns("Some test file");

            var viewModel = new UploadDocumentViewModel()
                            {
                                CandidateId = 21341234,
                                Document = postedFile,
                            };

            // Act
            var result = this.CandidateController.Upload(viewModel);

            // Assert
            this.CandidateService
                .Received()
                .Upload(Arg.Is<UploadDocumentRequest>(
                    r =>
                        r.CandidateId == viewModel.CandidateId && r.Document == viewModel.Document.InputStream &&
                        r.FileName == viewModel.Document.FileName));

            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            var redirect = result as RedirectToRouteResult;
            Assert.That(redirect.RouteValues["action"], Is.EqualTo("Details"));
            Assert.That(redirect.RouteValues["id"], Is.EqualTo(viewModel.CandidateId));
        }

        [Test]
        public void DeleteDocument()
        {
            // Arrange
            const int id = 3143214;
            var viewModel = new DeleteDocumentViewModel()
                            {
                                CandidateId = 3214312,
                                DocumentId = id,
                            };
            // Act
            var result = this.CandidateController.DeleteDocument(viewModel);

            // Assert
            this.CandidateService.Received().Delete(id);

            Assert.That(result, Is.InstanceOf<RedirectToRouteResult>());
            var redirect = result as RedirectToRouteResult;
            Assert.That(redirect.RouteValues["action"], Is.EqualTo("Details"));
            Assert.That(redirect.RouteValues["id"], Is.EqualTo(viewModel.CandidateId));
        }

        [Test]
        public void AddNote()
        {
            // Arrange
            var viewModel = new AddNoteViewModel
                            {
                                CandidateId = 54564789,
                                CandidateStatusId = 21234132,
                                Text = Guid.NewGuid().ToString(),
                            };

            this.CandidateService.AddNote(Arg.Any<AddNoteRequest>()).Returns(new ValidatedResponse());

            // Act
            var response = this.CandidateController.AddNote(viewModel);

            // Assert
            this.CandidateService
                .Received()
                .AddNote(Arg
                    .Is<AddNoteRequest>(
                        req => req.CandidateStatusId == viewModel.CandidateStatusId && req.Text == viewModel.Text));

            response.AssertRedirect("Details", viewModel.CandidateId.ToString());
        }


        [Test]
        public void AddNoteWithValidationErrors()
        {
            // Arrange
            var viewModel = new AddNoteViewModel
                            {
                                CandidateId = 54564789,
                                CandidateStatusId = 21234132,
                                Text = Guid.NewGuid().ToString(),
                            };

            var validationResult = new ValidationResult()
                                   {
                                       PropertyName = "Test 1",
                                       Message = "Ooops!"
                                   };
            this.CandidateService.AddNote(Arg.Any<AddNoteRequest>())
                .Returns(new ValidatedResponse()
                         {
                             ValidationResults = new[]
                                                 {
                                                     validationResult,
                                                 }
                         });

            // Act
            var response = this.CandidateController.AddNote(viewModel);

            // Assert
            this.CandidateService
                .Received()
                .AddNote(Arg
                    .Is<AddNoteRequest>(
                        req => req.CandidateStatusId == viewModel.CandidateStatusId && req.Text == viewModel.Text));

            response.AssertView("OperationFailed");
            this.CandidateController.AssertModelStateError(validationResult.PropertyName, validationResult.Message);
        }

        [Test]
        public void DeleteNote()
        {
            // Arrange
            var viewModel = new DeleteNoteViewModel()
            {
                CandidateId = 1234123,
                NoteId = 324412312,
            };

            this.CandidateService.DeleteNote(viewModel.NoteId).Returns(new ValidatedResponse());

            // Act
            var response = this.CandidateController.DeleteNote(viewModel);

            // Assert
            this.CandidateService
                .Received()
                .DeleteNote(viewModel.NoteId);

            response.AssertRedirect("Details", viewModel.CandidateId.ToString());
        }

        [Test]
        public void DeleteNoteWithValidationErrors()
        {
            // Arrange
            var viewModel = new DeleteNoteViewModel()
            {
                CandidateId = 1234123,
                NoteId = 324412312,
            };

            var validationResult = new ValidationResult()
            {
                PropertyName = "Test 1",
                Message = "Ooops!"
            };
            this.CandidateService.DeleteNote(viewModel.NoteId)
                .Returns(new ValidatedResponse()
                {
                    ValidationResults = new[]
                                                 {
                                                     validationResult,
                                                 }
                });

            // Act
            var response = this.CandidateController.DeleteNote(viewModel);

            // Assert
            this.CandidateService
                .Received()
                .DeleteNote(viewModel.NoteId);

            response.AssertView("OperationFailed");
            this.CandidateController.AssertModelStateError(validationResult.PropertyName, validationResult.Message);
        }

    }
}