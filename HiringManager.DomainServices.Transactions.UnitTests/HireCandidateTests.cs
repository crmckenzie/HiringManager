using System.Linq;
using FizzWare.NBuilder;
using HiringManager.Domain;
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
    }
}
