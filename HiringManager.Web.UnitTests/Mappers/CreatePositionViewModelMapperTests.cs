using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.Web.Mappers.Positions;
using HiringManager.Web.Models.Positions;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.Mappers
{
    [TestFixture]
    public class CreatePositionViewModelMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.UserSession = Substitute.For<IUserSession>();
            this.Mapper = new CreatePositionViewModelMapper(this.UserSession);
        }

        public IUserSession UserSession { get; set; }

        public IPrincipal Principal { get; set; }

        public IRepository Repository { get; set; }

        public CreatePositionViewModelMapper Mapper { get; set; }

        [Test]
        public void Map()
        {
            // Arrange
            var managerId = 5;
            this.UserSession.ManagerId.Returns(managerId);

            var viewModel = Builder<CreatePositionViewModel>
                .CreateNew()
                .Build()
                ;

            // Act
            var result = this.Mapper.Map(viewModel);

            // Assert
            Assert.That(result, Is.Not.Null);

            Assert.That(result.OpenDate, Is.EqualTo(viewModel.OpenDate));
            Assert.That(result.Title, Is.EqualTo(viewModel.Title));
            Assert.That(result.HiringManagerId, Is.EqualTo(managerId));
        }

    }
}
