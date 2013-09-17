using System.Linq;
using FizzWare.NBuilder;
using HiringManager.DomainServices.Impl;
using HiringManager.Mappers;
using HiringManager.Transactions;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.UnitTests
{
    [TestFixture]
    public class PositionServiceTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Transaction = Substitute.For<ITransaction<QueryPositionSummariesRequest, QueryResponse<PositionSummary>>>();
            this.TransactionBuilder = Substitute.For<IFluentTransactionBuilder>();
            this.Mapper = Substitute.For<IFluentMapper>();

            this.TransactionBuilder
                .Receives<QueryPositionSummariesRequest>()
                .Returns<QueryResponse<PositionSummary>>()
                .WithAuthorization()
                .WithRequestValidation()
                .WithPerformanceLogging()
                .Build()
                .Returns(this.Transaction)
                ;

            this.PositionService = new PositionService(this.TransactionBuilder, this.Mapper);
        }

        public IFluentMapper Mapper { get; set; }

        public IFluentTransactionBuilder TransactionBuilder { get; set; }

        public ITransaction<QueryPositionSummariesRequest, QueryResponse<PositionSummary>> Transaction { get; set; }

        public PositionService PositionService { get; set; }

        [Test]
        public void Query()
        {
            // Arrange

            var request = new QueryPositionSummariesRequest();
            var queryResponse = new QueryResponse<PositionSummary>();

            this.Transaction
                .Execute(request)
                .Returns(queryResponse)
                ;


            // Act
            var summaries = this.PositionService.Query(request);

            // Assert
            Assert.That(summaries, Is.SameAs(queryResponse));
        }

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
            var response = this.PositionService.Hire(request);

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
            var response = this.PositionService.CreatePosition(request);

            // Assert
            Assert.That(response, Is.SameAs(expectedResponse));
        }

    }
}
