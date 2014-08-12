using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;
using Simple.Validation;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class DeleteNoteTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.ValidationEngine = Substitute.For<IValidationEngine>();
            this.UnitOfWork = Substitute.For<IUnitOfWork>();
            this.DbContext = this.UnitOfWork.NewDbContext();
            this.Transaction = new DeleteNote(this.UnitOfWork, this.ValidationEngine);
        }

        public IValidationEngine ValidationEngine { get; set; }

        public IDbContext DbContext { get; set; }

        public IUnitOfWork UnitOfWork { get; set; }

        public DeleteNote Transaction { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            const int id = 32412341;

            // Act
            this.Transaction.Execute(new DeleteNoteRequest()
                                     {
                                         NoteId = id
                                     });

            // Assert
            this.DbContext.Received().Delete(Arg.Is<Note>(n => n.NoteId == id));
            this.DbContext.Received().SaveChanges();
            this.DbContext.Received().Dispose();
        }

        [Test]
        public void Execute_WithValidationErrors()
        {
            // Arrange
            const int id = 32412341;

            var request = new DeleteNoteRequest()
                                    {
                                        NoteId = id,
                                    };

            this.ValidationEngine.Validate(request)
                .Returns(new[] { new ValidationResult(), });


            // Act
            this.Transaction.Execute(request);

            // Assert
            this.DbContext.DidNotReceive().Delete(Arg.Any<Note>());
            this.DbContext.DidNotReceive().SaveChanges();
            this.DbContext.DidNotReceive().Dispose();
        }
    }
}