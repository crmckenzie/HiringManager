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
            this.DbContext = Substitute.For<IDbContext>();
            this.Clock = Substitute.For<IClock>();
            this.Command = new HireCandidate(this.DbContext, this.Clock);
        }

        public IClock Clock { get; set; }

        public HireCandidate Command { get; set; }

        public IDbContext DbContext { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            var candidate = Builder<Candidate>
                .CreateNew()
                .Build()
                ;

            var opening = new Opening();
            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.Openings.Add(opening))
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



            this.DbContext.Get<CandidateStatus>(candidateStatus.CandidateStatusId.Value).Returns(candidateStatus);

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
            
            Assert.That(opening.FilledBy, Is.SameAs(candidate));
            Assert.That(opening.FilledDate, Is.EqualTo(Clock.Now));
            Assert.That(opening.Status, Is.EqualTo("Filled"));
            Assert.That(position.Status, Is.EqualTo("Filled"));

            this.DbContext.Received().SaveChanges();
        }

        [Test]
        public void OtherCandidatesAreMarkedPassed()
        {
            // Arrange
            var candidate = Builder<Candidate>
                .CreateNew()
                .Build()
                ;

            var opening = new Opening();
            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.Openings.Add(opening))
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
            
            this.DbContext.Get<CandidateStatus>(hiredStatus.CandidateStatusId.Value).Returns(hiredStatus);

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
