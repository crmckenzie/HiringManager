using System.Linq;
using FizzWare.NBuilder;
using HiringManager.Domain;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class PassOnCandidateTests
    {
        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Repository = Substitute.For<IRepository>();
            this.Command = new PassOnCandidate(this.Repository, this.Clock);
        }

        public IClock Clock { get; set; }

        public PassOnCandidate Command { get; set; }

        public IRepository Repository { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange

            var candidateStatus = Builder<CandidateStatus>
                .CreateNew()
                .Do(arg =>
                    {
                        arg.Status = "Applied";
                    })
                .Build()
                ;

            this.Repository.Get<CandidateStatus>(candidateStatus.CandidateStatusId.Value).Returns(candidateStatus);

            // Act
            var request = new PassOnCandidateRequest()
                          {
                              CandidateStatusId = candidateStatus.CandidateStatusId.Value
                          };
            var response = this.Command.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.CandidateStatusId, Is.EqualTo(candidateStatus.CandidateStatusId));
            Assert.That(response.Status, Is.EqualTo("Passed"));

            this.Repository.Received().Commit();
        }
    }
}