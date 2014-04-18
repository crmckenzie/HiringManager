using FizzWare.NBuilder;
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
            this.DbContext = Substitute.For<IDbContext>();
            this.AddCandidate = new AddCandidate(this.DbContext);
        }

        public IDbContext DbContext { get; set; }

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

            this.DbContext.When(r => r.Add(Arg.Any<CandidateStatus>()))
                .Do(arg => arg.Arg<CandidateStatus>().CandidateStatusId = candidateStatusId);


            this.DbContext.When(r => r.Add(Arg.Any<Candidate>()))
                .Do(arg => arg.Arg<Candidate>().CandidateId = candidateId);


            // Act
            var response = this.AddCandidate.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);

            this.DbContext.Received()
                .Add(Arg.Is<Candidate>(arg => arg.Name == request.CandidateName));

            foreach (var contactInfo in request.ContactInfo)
            {
                this.DbContext.Received()
                    .Add(Arg.Is<ContactInfo>(arg => arg.Type == contactInfo.Type && arg.Value == contactInfo.Value && arg.Candidate != null));
                
            }

            this.DbContext
                .Received()
                .Add(Arg.Is<CandidateStatus>(arg=> arg.PositionId == request.PositionId && arg.Status == "Resume Received"))
                ;


            Assert.That(response.PositionId, Is.EqualTo(request.PositionId));
            Assert.That(response.CandidateStatusId, Is.EqualTo(candidateStatusId));
            Assert.That(response.CandidateId, Is.EqualTo(candidateId));
        }

    }
}
