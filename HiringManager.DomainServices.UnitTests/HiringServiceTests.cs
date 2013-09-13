using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.Transactions;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.UnitTests
{
    [TestFixture]
    public class HiringServiceTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.TransactionBuilder = Substitute.For<IFluentTransactionBuilder>();
            this.HiringService = new HiringService(this.TransactionBuilder);
        }

        public IFluentTransactionBuilder TransactionBuilder { get; set; }

        public HiringService HiringService { get; set; }

        [Test]
        public void Hire()
        {
            // Arrange
            var expectedResponse = Builder<HireCandidateResponse>
                .CreateNew()
                .Build()
                ;

            var request = Builder<HireCandidateRequest>
                .CreateNew()
                .Build()
                ;

            var hireCandidate = Substitute.For<ITransaction<HireCandidateRequest, HireCandidateResponse>>();
            hireCandidate.Execute(request).Returns(expectedResponse);

            this.TransactionBuilder
                .Receives<HireCandidateRequest>()
                .Returns<HireCandidateResponse>()
                .WithRequestValidation()
                .WithAuthorization()
                .WithPerformanceLogging()
                .Build()
                .Returns(hireCandidate)
                ;

            // Act
            var response = this.HiringService.Hire(request);

            // Assert
            Assert.That(response, Is.SameAs(expectedResponse));
        }

        [Test]
        public void CreatePosition()
        {
            // Arrange
            var expectedResponse = Builder<CreatePositionResponse>
                .CreateNew()
                .Build()
                ;

            var request = Builder<CreatePositionRequest>
                .CreateNew()
                .Build()
                ;

            var createPosition = Substitute.For<ITransaction<CreatePositionRequest, CreatePositionResponse>>();
            createPosition.Execute(request).Returns(expectedResponse);

            this.TransactionBuilder
                .Receives<CreatePositionRequest>()
                .Returns<CreatePositionResponse>()
                .WithRequestValidation()
                .WithAuthorization()
                .WithPerformanceLogging()
                .Build()
                .Returns(createPosition)
                ;

            // Act
            var response = this.HiringService.CreatePosition(request);

            // Assert
            Assert.That(response, Is.SameAs(expectedResponse));
        }

    }
}
