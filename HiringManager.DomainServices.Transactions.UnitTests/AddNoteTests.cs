using System;
using HiringManager.DomainServices.Authentication;
using HiringManager.DomainServices.Candidates;
using HiringManager.EntityModel;
using NUnit.Framework;
using NSubstitute;
using Simple.Validation;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class AddNoteTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.ValidationEngine = Substitute.For<IValidationEngine>();
            this.Clock = Substitute.For<IClock>();
            this.UnitOfWork = Substitute.For<IUnitOfWork>();
            this.DbContext = this.UnitOfWork.NewDbContext();
            this.UserSession = Substitute.For<IUserSession>();
            this.Transaction = new AddNote(this.UnitOfWork, this.UserSession, this.Clock, this.ValidationEngine);
        }

        public IValidationEngine ValidationEngine { get; set; }

        public IClock Clock { get; set; }

        public IDbContext DbContext { get; set; }

        public IUserSession UserSession { get; set; }

        public IUnitOfWork UnitOfWork { get; set; }

        public AddNote Transaction { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            this.UserSession.ManagerId.Returns(43121324);

            var request = new AddNoteRequest()
            {
                CandidateStatusId = 213412,
                Text = Guid.NewGuid().ToString(),
            };

            // Act
            this.Transaction.Execute(request);

            // Assert
            this.DbContext.Received()
                .Add(Arg.Is<Note>(
                        n =>
                            n.Text == request.Text &&
                            n.AuthorId == this.UserSession.ManagerId &&
                            n.CandidateStatusId == request.CandidateStatusId &&
                            n.Authored == this.Clock.Now));

            this.DbContext.Received().SaveChanges();
            this.DbContext.Received().Dispose();
        }

        [Test]
        public void Execute_WithValidationErrors()
        {
            // Arrange
            this.UserSession.ManagerId.Returns(43121324);

            var request = new AddNoteRequest()
            {
                CandidateStatusId = 213412,
                Text = Guid.NewGuid().ToString(),
            };
            this.ValidationEngine.Validate(request)
                .Returns(new[] { new ValidationResult(), });


            // Act
            this.Transaction.Execute(request);

            // Assert
            this.DbContext.DidNotReceive().Add(Arg.Any<Note>());
            this.DbContext.DidNotReceive().SaveChanges();
            this.DbContext.DidNotReceive().Dispose();
        }
    }
}
