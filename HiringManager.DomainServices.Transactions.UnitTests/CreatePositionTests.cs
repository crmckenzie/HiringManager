using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.Domain;
using HiringManager.DomainServices;
using HiringManager.DomainServices.Transactions;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.Transactions.UnitTests
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
            this.Transaction = new CreatePosition(this.Repository, this.FluentMapper);
        }

        public IFluentMapper FluentMapper { get; set; }

        public IRepository Repository { get; set; }

        public CreatePosition Transaction { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
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
        }

    }
}
