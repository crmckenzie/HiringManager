using System.Linq;
using FizzWare.NBuilder;
using HiringManager.EntityModel;
using HiringManager.Web.Infrastructure.AutoMapper;
using NSubstitute;
using NUnit.Framework;
using Simple.Validation;

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
            this.ValidationEngine = Substitute.For<IValidationEngine>();
            this.Transaction = new CreatePosition(this.DbContext, this.ValidationEngine);
        }

        public IValidationEngine ValidationEngine { get; set; }

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

        [Test]
        public void WhenThereAreWarnings()
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

            var validationResults = new[]
                                    {
                                        new ValidationResult()
                                        {
                                            Severity = ValidationResultSeverity.Warning
                                        },
                                    };
            this.ValidationEngine.Validate(request).Returns(validationResults);


            // Act
            var response = this.Transaction.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            this.DbContext.Received().Add(Arg.Is<Position>(row => row.OpenDate == request.OpenDate && row.Title == request.Title));
            this.DbContext.Received().SaveChanges();

            Assert.That(response.PositionId, Is.EqualTo(1001));
            Assert.That(position.CreatedById, Is.EqualTo(request.HiringManagerId));
        }

        [Test]
        public void WhenRequestIsInvalid()
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

            var validationResults = new[]
                                    {
                                        new ValidationResult(),
                                    };
            this.ValidationEngine.Validate(request).Returns(validationResults);

            // Act
            var response = this.Transaction.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            this.DbContext.DidNotReceive().Add(Arg.Is<Position>(row => row.OpenDate == request.OpenDate && row.Title == request.Title));
            this.DbContext.DidNotReceive().SaveChanges();

            Assert.That(response.PositionId, Is.Null);
            Assert.That(position.CreatedById, Is.EqualTo(request.HiringManagerId));
            Assert.That(response.ValidationResults, Is.EqualTo(validationResults));
        }
    }
}
