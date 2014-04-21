using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.EntityFramework;
using HiringManager.Web.Controllers;
using HiringManager.Web.Infrastructure.AutoMapper;
using HiringManager.Web.ViewModels;
using HiringManager.Web.ViewModels.Candidates;
using NSubstitute;
using NUnit.Framework;

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
            this.DbContext = Substitute.For<HiringManagerDbContext>();
            this.CandidateController = new CandidateController(this.CandidateService, this.DbContext);
        }

        public ICandidateService CandidateService { get; set; }

        public HiringManagerDbContext DbContext { get; set; }

        public CandidateController CandidateController { get; set; }

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

        private static Expression<Predicate<SaveCandidateRequest>> MatchesViewModel(EditCandidateViewModel viewModel)
        {
            return req =>
                req.CandidateId == null && req.Name == viewModel.Name && req.SourceId == viewModel.SourceId;
        }
    }
}
