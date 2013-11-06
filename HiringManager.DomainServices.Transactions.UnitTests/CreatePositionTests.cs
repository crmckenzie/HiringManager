using FizzWare.NBuilder;
using HiringManager.EntityModel;
using HiringManager.Mappers;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class CreatePositionTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Repository = Substitute.For<IRepository>();
            this.FluentMapper = Substitute.For<IFluentMapper>();
            this.UserSession = Substitute.For<IUserSession>();
            this.Transaction = new CreatePosition(this.Repository, this.FluentMapper, this.UserSession);
        }

        public IUserSession UserSession { get; set; }

        public IFluentMapper FluentMapper { get; set; }

        public IRepository Repository { get; set; }

        public CreatePosition Transaction { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            this.UserSession.ManagerId.Returns(25);
            var manager = Builder<Manager>.CreateNew().Build();
            this.Repository.Get<Manager>(25).Returns(manager);

            var position = Builder<Position>
                .CreateNew()
                .Build()
                ;

            var request = new CreatePositionRequest();

            this.FluentMapper
                .Map<Position>()
                .From(request)
                .Returns(position)
                ;

            // Act
            var response = this.Transaction.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            this.Repository.Received().Store(position);
            this.Repository.Received().Commit();

            Assert.That(response.PositionId, Is.EqualTo(position.PositionId));
            Assert.That(position.CreatedBy, Is.EqualTo(manager));
        }

    }
}
