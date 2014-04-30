using FizzWare.NBuilder;
using HiringManager.DomainServices.AutoMapperProfiles;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class AddCandidateToPositionTests
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
            this.AddCandidateToPosition = new AddCandidateToPosition(this.Db);
        }

        public IDbContext Db { get; set; }

        public AddCandidateToPosition AddCandidateToPosition { get; set; }

        [Test]
        public void Execute_WithNewCandidate()
        {
            // Arrange
            var request = Builder<AddCandidateRequest>
                .CreateNew()
                .Do(row => row.CandidateId = null)
                .Do(row => row.ContactInfo = Builder<ContactInfoDetails>.CreateListOfSize(2).Build())
                .Build()
                ;

            const int candidateStatusId = 3;
            const int candidateId = 4;

            this.Db.When(r => r.Add(Arg.Any<Candidate>()))
                .Do(arg => arg.Arg<Candidate>().CandidateId = candidateId);

            this.Db.When(r => r.Add(Arg.Any<CandidateStatus>()))
                .Do(arg =>
                    {
                        arg.Arg<CandidateStatus>().CandidateStatusId = candidateStatusId;
                        arg.Arg<CandidateStatus>().CandidateId = candidateId;
                    });

            // Act
            var response = this.AddCandidateToPosition.Execute(request);

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
                .Add(Arg.Is<CandidateStatus>(arg => arg.PositionId == request.PositionId && arg.Status == "Resume Received"))
                ;


            Assert.That(response.PositionId, Is.EqualTo(request.PositionId));
            Assert.That(response.CandidateStatusId, Is.EqualTo(candidateStatusId));
            Assert.That(response.CandidateId, Is.EqualTo(candidateId));
        }

        [Test]
        public void Execute_WithExistingCandidate()
        {
            const int candidateStatusId = 3;
            const int candidateId = 4;

            // Arrange
            var request = Builder<AddCandidateRequest>
                .CreateNew()
                .Do(row => row.CandidateId = candidateId)
                .Do(row => row.ContactInfo = Builder<ContactInfoDetails>.CreateListOfSize(2).Build())
                .Build()
                ;


            var candidate = new Candidate();
            this.Db.Get<Candidate>(candidateId).Returns(candidate);

            this.Db.When(r => r.Add(Arg.Any<CandidateStatus>()))
                .Do(arg => arg.Arg<CandidateStatus>().CandidateStatusId = candidateStatusId);

            // Act
            var response = this.AddCandidateToPosition.Execute(request);

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
