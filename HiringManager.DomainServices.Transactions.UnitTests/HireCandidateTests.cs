using System.Linq;
using FizzWare.NBuilder;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class HireCandidateTests
    {
        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Repository = Substitute.For<IRepository>();
            this.Clock = Substitute.For<IClock>();
            this.Command = new HireCandidate(this.Repository, this.Clock);
        }

        public IClock Clock { get; set; }

        public HireCandidate Command { get; set; }

        public IRepository Repository { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            var candidate = Builder<Candidate>
                .CreateNew()
                .Build()
                ;

            var position = Builder<Position>
                .CreateNew()
                .Build()
                ;

            var candidateStatus = Builder<CandidateStatus>
                .CreateNew()
                .Do(arg =>
                    {
                        arg.Candidate = candidate;
                        arg.Position = position;
                        arg.Status = "Applied";

                        candidate.AppliedTo.Add(arg);
                        position.Candidates.Add(arg);
                    })
                .Build()
                ;



            this.Repository.Get<CandidateStatus>(candidateStatus.CandidateStatusId.Value).Returns(candidateStatus);

            // Act
            var request = new HireCandidateRequest()
                          {
                              CandidateStatusId = candidateStatus.CandidateStatusId.Value
                          };
            var response = this.Command.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.CandidateStatusId, Is.EqualTo(candidateStatus.CandidateStatusId));
            Assert.That(response.Status, Is.EqualTo("Hired"));

            Assert.That(position.FilledBy, Is.SameAs(candidate));
            Assert.That(position.FilledDate, Is.EqualTo(Clock.Now));
            Assert.That(position.Status, Is.EqualTo("Filled"));

            this.Repository.Received().Commit();
        }

        [Test]
        public void OtherCandidatesAreMarkedPassed()
        {
            // Arrange
            var candidate = Builder<Candidate>
                .CreateNew()
                .Build()
                ;

            var position = Builder<Position>
                .CreateNew()
                .Build()
                ;

            var candidateStatuses = Builder<CandidateStatus>
                .CreateListOfSize(3)
                .All()
                .Do(arg =>
                {
                    arg.Candidate = candidate;
                    arg.Position = position;
                    arg.Status = "Applied";

                    candidate.AppliedTo.Add(arg);
                    position.Candidates.Add(arg);
                })
                .Build()
                ;

            var hiredStatus = candidateStatuses.Skip(1).First();
            
            this.Repository.Get<CandidateStatus>(hiredStatus.CandidateStatusId.Value).Returns(hiredStatus);

            // Act
            var request = new HireCandidateRequest()
            {
                CandidateStatusId = hiredStatus.CandidateStatusId.Value
            };
            var response = this.Command.Execute(request);

            // Assert
            candidateStatuses.Remove(hiredStatus);

            foreach (var candidateStatus in candidateStatuses)
            {
                Assert.That(candidateStatus.Status, Is.EqualTo("Passed"));
            }

        }
    }
}
