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
            this.DbContext = Substitute.For<IDbContext>();
            this.Transaction = new CreatePosition(this.DbContext);
        }

        public IDbContext DbContext { get; set; }

        public CreatePosition Transaction { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            var manager = Builder<Manager>.CreateNew().Build();
            this.DbContext.Get<Manager>(25).Returns(manager);

            this.DbContext.WhenForAnyArgs(r => r.Add<Position>(null))
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
            this.DbContext.Received().Add(Arg.Is<Position>(row => row.OpenDate == request.OpenDate && row.Title == request.Title));
            this.DbContext.Received().SaveChanges();

            Assert.That(response.PositionId, Is.EqualTo(1001));
            Assert.That(position.CreatedById, Is.EqualTo(request.HiringManagerId));
        }

    }
}
