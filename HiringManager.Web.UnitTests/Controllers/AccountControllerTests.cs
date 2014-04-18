using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.Web.Controllers;
using HiringManager.Web.Models.Accounts;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.Controllers
{
    [TestFixture]
    public class AccountControllerTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        public static HttpContextBase FakeHttpContext()
        {
            var request = Substitute.For<HttpRequestBase>();
            var response = Substitute.For<HttpResponseBase>();
            var session = Substitute.For<HttpSessionStateBase>();
            var server = Substitute.For<HttpServerUtilityBase>();

            var context = Substitute.For<HttpContextBase>();

            context.Request.Returns(request);
            context.Response.Returns(response);
            context.Session.Returns(session);
            context.Server.Returns(server);

            return context;
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.UserManager = Substitute.For<IUserManager>();
            this.AuthenticationManager = Substitute.For<IAuthenticationManager>();
            this.Controller = new AccountController(this.UserManager, this.AuthenticationManager);

            var fakeHttpContext = FakeHttpContext();
            this.Controller.Url = new UrlHelper(new RequestContext(fakeHttpContext, new RouteData()));
        }

        public IUserManager UserManager { get; set; }

        public IAuthenticationManager AuthenticationManager { get; set; }

        public AccountController Controller { get; set; }

        [Test]
        public void Login()
        {
            // Arrange
            var loginViewModel = Builder<LoginViewModel>
                .CreateNew()
                .Build()
                ;
            var applicationUser = Builder<ApplicationUser>
                .CreateNew()
                .Build()
                ;

            var identity = Builder<ClaimsIdentity>
                .CreateNew()
                .Build()
                ;

            this.UserManager.FindAsync(loginViewModel.UserName, loginViewModel.Password)
                .Returns(Task.FromResult(applicationUser))
                ;

            this.UserManager.CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie)
                .Returns(Task.FromResult(identity))
                ;

            // Act
            const string returnUrl = "~/SomeUrl";
            var actionResult = this.Controller.Login(loginViewModel, returnUrl: returnUrl)
                .ExecuteSynchronously()
                ;

            // Assert
            Assert.That(actionResult, Is.Not.Null);
            var redirect = actionResult as RedirectResult;
            Assert.That(redirect, Is.Not.Null);
            Assert.That(redirect.Url, Is.EqualTo(returnUrl));
        }



        [Test]
        public void WhenLoginFails()
        {
            // Arrange
            var loginViewModel = Builder<LoginViewModel>
                .CreateNew()
                .Build()
                ;
            var applicationUser = Builder<ApplicationUser>
                .CreateNew()
                .Build()
                ;

            var identity = Builder<ClaimsIdentity>
                .CreateNew()
                .Build()
                ;

            this.UserManager.FindAsync(loginViewModel.UserName, loginViewModel.Password)
                .Returns(Task.FromResult<ApplicationUser>(null))
                ;

            this.UserManager.CreateIdentityAsync(applicationUser, DefaultAuthenticationTypes.ApplicationCookie)
                .Returns(Task.FromResult(identity))
                ;

            // Act
            const string returnUrl = "~/SomeUrl";
            var actionResult = this.Controller.Login(loginViewModel, returnUrl: returnUrl)
                .ExecuteSynchronously()
                ;

            // Assert
            Assert.That(actionResult, Is.Not.Null);
            var viewResult = actionResult as ViewResult;
            Assert.That(viewResult, Is.Not.Null);
            Assert.That(viewResult.Model, Is.SameAs(loginViewModel));
            Assert.That(this.Controller.ModelState.Values.Count, Is.EqualTo(1));
            Assert.That(this.Controller.ModelState.Values.First().Errors.First().ErrorMessage, Is.EqualTo("Invalid username or password."));
        }

    }
}
