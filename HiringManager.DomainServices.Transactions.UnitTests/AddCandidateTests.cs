using FizzWare.NBuilder;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class AddCandidateTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Db = Substitute.For<IDbContext>();
            this.AddCandidate = new AddCandidate(this.Db);
        }

        public IDbContext Db { get; set; }

        public AddCandidate AddCandidate { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            var request = Builder<AddCandidateRequest>
                .CreateNew()
                .Do(row => row.ContactInfo = Builder<ContactInfoDetails>.CreateListOfSize(2).Build())
                .Build()
                ;

            const int candidateStatusId = 3;
            const int candidateId = 4;

            this.Db.When(r => r.Add(Arg.Any<CandidateStatus>()))
                .Do(arg => arg.Arg<CandidateStatus>().CandidateStatusId = candidateStatusId);


            this.Db.When(r => r.Add(Arg.Any<Candidate>()))
                .Do(arg => arg.Arg<Candidate>().CandidateId = candidateId);

            // Act
            var response = this.AddCandidate.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);

            this.Db.Received()
                .Add(Arg.Is<Candidate>(arg => arg.Name == request.CandidateName && arg.SourceId == request.SourceId));

            foreach (var contactInfo in request.ContactInfo)
            {
                this.Db.Received()
                    .Add(Arg.Is<ContactInfo>(arg => arg.Type == contactInfo.Type && arg.Value == contactInfo.Value && arg.Candidate != null));
                
            }

            this.Db
                .Received()
                .Add(Arg.Is<CandidateStatus>(arg=> arg.PositionId == request.PositionId && arg.Status == "Resume Received"))
                ;


            Assert.That(response.PositionId, Is.EqualTo(request.PositionId));
            Assert.That(response.CandidateStatusId, Is.EqualTo(candidateStatusId));
            Assert.That(response.CandidateId, Is.EqualTo(candidateId));
        }

    }
}
