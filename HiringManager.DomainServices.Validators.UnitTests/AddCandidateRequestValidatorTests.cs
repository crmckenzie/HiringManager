using FizzWare.NBuilder;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;
using Simple.Validation;

namespace HiringManager.DomainServices.Validators.UnitTests
{
    [TestFixture]
    public class AddCandidateRequestValidatorTests
    {
        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.DbContext = Substitute.For<IDbContext>();
            this.Validator = new AddCandidateRequestValidator(this.DbContext);
        }

        public IDbContext DbContext { get; set; }

        public AddCandidateRequestValidator Validator { get; set; }

        [Test]
        public void AppliesTo()
        {
            // Arrange

            // Act
            var appliesTo = this.Validator.AppliesTo(null);

            // Assert
            Assert.That(appliesTo, Is.True);
        }

        [Test]
        public void PositionIsNotFilled()
        {
            // Arrange
            var request = Builder<AddCandidateRequest>
                .CreateNew()
                .Build()
                ;

            var position = Builder<Position>
                .CreateNew()
                .Build()
                ;

            this.DbContext
                .Get<Position>(request.PositionId)
                .Returns(position)
                ;

            // Act
            var results = this.Validator.Validate(request);

            // Assert
            var template = new ValidationResult()
            {
                PropertyName = "PositionId",
                Severity = ValidationResultSeverity.Error,
            };

            results.AssertIsValidFor(template);

        }

        [Test]
        public void PositionIsFilled()
        {
            // Arrange
            var request = Builder<AddCandidateRequest>
                .CreateNew()
                .Build()
                ;

            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.FilledBy = new Candidate())
                .Build()
                ;

            this.DbContext
                .Get<Position>(request.PositionId)
                .Returns(position)
                ;

            // Act
            var results = this.Validator.Validate(request);

            // Assert
            var template = new ValidationResult()
                           {
                               PropertyName = "PositionId",
                               Severity = ValidationResultSeverity.Error,
                               Message = "Cannot add a candidate to a filled position.",
                           };

            results.AssertInvalidFor(template);
        }

    }
}
