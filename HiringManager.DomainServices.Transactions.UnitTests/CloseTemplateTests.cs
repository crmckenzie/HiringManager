using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;
using Simple.Validation;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class CloseTemplateTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Repository = Substitute.For<IRepository>();
            this.ValidationEngine = Substitute.For<IValidationEngine>();
            this.Transaction = new ClosePosition(this.Repository, this.ValidationEngine);
        }

        public IValidationEngine ValidationEngine { get; set; }

        public IRepository Repository { get; set; }

        public ClosePosition Transaction { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            var position = Builder<Position>
                .CreateNew()
                .Build()
                ;

            this.Repository.Get<Position>(position.PositionId.Value).Returns(position);

            // Act
            var response = this.Transaction.Execute(position.PositionId.Value);

            // Assert
            Assert.That(response, Is.Not.Null);
            this.Repository.Received().Store(position);
            this.Repository.Received().Commit();

            Assert.That(position.Status, Is.EqualTo("Closed"));
        }

        [Test]
        public void Execute_WhenPositionHasCandidates()
        {
            // Arrange
            var statuses = Builder<CandidateStatus>
                .CreateListOfSize(3)
                .Build()
                .ToList()
                ;
            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.Candidates = statuses)
                .Build()
                ;

            this.Repository.Get<Position>(position.PositionId.Value).Returns(position);

            // Act
            var response = this.Transaction.Execute(position.PositionId.Value);

            // Assert
            foreach (var status in statuses)
            {
                this.Repository.Received().Store(status);
                Assert.That(status.Status, Is.EqualTo("Passed"));
            }

        }

        [Test]
        public void Execute_WithValidationErrors()
        {
            // Arrange
            var statuses = Builder<CandidateStatus>
                .CreateListOfSize(3)
                .Build()
                .ToList()
                ;
            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.Candidates = statuses)
                .Build()
                ;

            var validationResults = new[]
                                    {
                                        new ValidationResult(),
                                    };
            this.ValidationEngine.Validate(position, "Close").Returns(validationResults);

            this.Repository.Get<Position>(position.PositionId.Value).Returns(position);

            // Act
            var response = this.Transaction.Execute(position.PositionId.Value);

            // Assert
            Assert.That(response.ValidationResults, Is.EquivalentTo(validationResults));
            this.Repository.DidNotReceive().Store(position);
            this.Repository.DidNotReceive().Commit();
        }
    }
}
