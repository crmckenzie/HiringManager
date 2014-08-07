using System.Linq;
using FizzWare.NBuilder;
using HiringManager.DomainServices.AutoMapperProfiles;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class AddExistingCandidateToPositionTests
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
            this.Db = Substitute.For<IDbContext>();
            this.Transaction = new AddExistingCandidateToPosition(this.Db);
        }

        public IDbContext Db { get; set; }

        public AddExistingCandidateToPosition Transaction { get; set; }

        [Test]
        public void Execute()
        {
            const int candidateStatusId = 3;
            const int candidateId = 4;

            // Arrange
            var request = Builder<AddCandidateRequest>
                .CreateNew()
                .Do(row => row.CandidateId = candidateId)
                .Build()
                ;


            var candidate = new Candidate();
            this.Db.Get<Candidate>(candidateId).Returns(candidate);

            this.Db.When(r => r.Add(Arg.Any<CandidateStatus>()))
                .Do(arg => arg.Arg<CandidateStatus>().CandidateStatusId = candidateStatusId);

            // Act
            var response = this.Transaction.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);

            this.Db
                .Received()
                .Add(Arg.Is<CandidateStatus>(arg => arg.PositionId == request.PositionId && arg.Status == "Resume Received" && arg.Candidate == candidate))
                ;


            Assert.That(response.PositionId, Is.EqualTo(request.PositionId));
            Assert.That(response.CandidateStatusId, Is.EqualTo(candidateStatusId));
            Assert.That(response.CandidateId, Is.EqualTo(candidateId));
        }

    }
}