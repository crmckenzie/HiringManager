using FizzWare.NBuilder;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class SetCandidateStatusTests
    {
        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.DbContext = Substitute.For<IDbContext>();
            this.Command = new SetCandidateStatus(this.DbContext);
        }

        public IClock Clock { get; set; }

        public SetCandidateStatus Command { get; set; }

        public IDbContext DbContext { get; set; }

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

            this.DbContext.Get<CandidateStatus>(candidateStatus.CandidateStatusId.Value).Returns(candidateStatus);

            // Act
            var request = new SetCandidateStatusRequest()
                          {
                              CandidateStatusId = candidateStatus.CandidateStatusId.Value,
                              Status = "Passed"
                          };
            var response = this.Command.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.CandidateStatusId, Is.EqualTo(candidateStatus.CandidateStatusId));
            Assert.That(response.Status, Is.EqualTo("Passed"));

            this.DbContext.Received().SaveChanges();
        }
    }
}