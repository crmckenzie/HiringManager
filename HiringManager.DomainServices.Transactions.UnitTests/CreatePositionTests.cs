using FizzWare.NBuilder;
using HiringManager.EntityModel;
using HiringManager.Web.Infrastructure.AutoMapper;
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
            AutoMapperConfiguration.Configure();
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Repository = Substitute.For<IRepository>();
            this.Transaction = new CreatePosition(this.Repository);
        }

        public IRepository Repository { get; set; }

        public CreatePosition Transaction { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            var manager = Builder<Manager>.CreateNew().Build();
            this.Repository.Get<Manager>(25).Returns(manager);

            this.Repository.WhenForAnyArgs(r => r.Store<Position>(null))
                .Do(ci => ci.Arg<Position>().PositionId = 1001);

            var position = Builder<Position>
                .CreateNew()
                .Build()
                ;

            var request = Builder<CreatePositionRequest>
                .CreateNew()
                .Build()
                ;

            // Act
            var response = this.Transaction.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            this.Repository.Received().Store(Arg.Is<Position>(row => row.OpenDate == request.OpenDate && row.Title == request.Title));
            this.Repository.Received().Commit();

            Assert.That(response.PositionId, Is.EqualTo(1001));
            Assert.That(position.CreatedById, Is.EqualTo(request.HiringManagerId));
        }

    }
}
