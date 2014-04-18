using FizzWare.NBuilder;
using HiringManager.DomainServices.AutoMapperProfiles;
using HiringManager.DomainServices.Impl;
using HiringManager.EntityModel;
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
            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.AddProfile<DomainProfile>();
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.TransactionBuilder = Substitute.For<IFluentTransactionBuilder>();
            this.UnitOfWork = Substitute.For<IUnitOfWork>();
            this.DbContext = this.UnitOfWork.NewDbContext();

            this.PositionService = new PositionService(this.TransactionBuilder, this.UnitOfWork);
        }

        public IUnitOfWork UnitOfWork { get; set; }

        public IDbContext DbContext { get; set; }

        public IFluentTransactionBuilder TransactionBuilder { get; set; }

        public ITransaction<QueryPositionSummariesRequest, QueryResponse<PositionSummary>> Transaction { get; set; }

        public PositionService PositionService { get; set; }

        [Test]
        public void Query()
        {
            // Arrange
            var request = new QueryPositionSummariesRequest();
            var queryResponse = new QueryResponse<PositionSummary>();
            var transaction = Substitute.For<ITransaction<QueryPositionSummariesRequest, QueryResponse<PositionSummary>>>();

            this.TransactionBuilder
                .Receives<QueryPositionSummariesRequest>()
                .Returns<QueryResponse<PositionSummary>>()
                .WithRequestValidation()
                .WithPerformanceLogging()
                .Build()
                .Returns(transaction)
                ;

            transaction
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
            var expectedResponse = Builder<CandidateStatusResponse>
                .CreateNew()
                .Build()
                ;
            var hireCandidate = Substitute.For<ITransaction<HireCandidateRequest, CandidateStatusResponse>>();
            const int candidateStatusId = 1;
            hireCandidate.Execute(Arg.Is<HireCandidateRequest>(arg => arg.CandidateStatusId == candidateStatusId)).Returns(expectedResponse);

            this.TransactionBuilder
                .Receives<HireCandidateRequest>()
                .Returns<CandidateStatusResponse>()
                .WithRequestValidation()
                .WithPerformanceLogging()
                .Build()
                .Returns(hireCandidate)
                ;

            // Act
            var response = this.PositionService.Hire(candidateStatusId);

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
                .WithPerformanceLogging()
                .Build()
                .Returns(createPosition)
                ;

            // Act
            var response = this.PositionService.CreatePosition(request);

            // Assert
            Assert.That(response, Is.SameAs(expectedResponse));
        }

        [Test]
        public void Details()
        {
            // Arrange
            var position = Builder<Position>
                .CreateNew()
                .Build();

            this.DbContext
                .Get<Position>(1)
                .Returns(position);


            // Act
            var result = this.PositionService.Details(1);

            // Assert
            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void GetCandidateStatusDetails()
        {
            // Arrange
            var position = Builder<Position>
                .CreateNew()
                .Build()
                ;

            var candidate = Builder<Candidate>
                .CreateNew()
                .Build()
                ;

            var candidateStatus = Builder<CandidateStatus>
                .CreateNew()
                .Do(row => row.Position = position)
                .Do(row => row.Candidate = candidate)
                .Build()
                ;

            this.DbContext
                .Get<CandidateStatus>(candidateStatus.CandidateStatusId.Value)
                .Returns(candidateStatus);

            // Act
            var result = this.PositionService.GetCandidateStatusDetails(candidateStatus.CandidateStatusId.Value);

            // Assert
            Assert.That(result, Is.Not.Null);

        }
    }
}
