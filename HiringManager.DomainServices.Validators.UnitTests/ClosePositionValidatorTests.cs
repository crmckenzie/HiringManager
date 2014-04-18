using System.Linq;
using FizzWare.NBuilder;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;
using Simple.Validation;

namespace HiringManager.DomainServices.Validators.UnitTests
{
    [TestFixture]
    public class ClosePositionValidatorTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Validator = new ClosePositionValidator();
        }

        public ClosePositionValidator Validator { get; set; }

        [Test]
        [TestCase("Close", true)]
        [TestCase("Hire", false)]
        [TestCase("", false)]
        [TestCase(null, false)]
        public void AppliesTo(string operation, bool expectedResult)
        {
            // Arrange

            // Act
            var result = this.Validator.AppliesTo(operation);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void Validate_NotFilled()
        {
            // Arrange
            var position = Builder<Position>
                .CreateNew()
                .Build()
                ;

            // Act
            var results = this.Validator.Validate(position).ToList();

            // Assert
            Assert.That(results, Is.Empty);
        }

        [Test]
        public void Validate_Filled()
        {
            // Arrange
            var position = Substitute.For<Position>();
            position.IsFilled().Returns(true);

            // Act
            var results = this.Validator.Validate(position).ToList();

            // Assert
            results.AssertInvalidFor(new ValidationResult()
                                     {
                                         PropertyName = "Status",
                                         Context = position,
                                         Severity = ValidationResultSeverity.Error,
                                     });
        }

        [Test]
        public void Validate_Closed()
        {
            // Arrange
            var position = Builder<Position>
                .CreateNew()
                .Do(row => row.Status = "Closed")
                .Build()
                ;

            // Act
            var results = this.Validator.Validate(position).ToList();

            // Assert
            results.AssertInvalidFor(new ValidationResult()
            {
                PropertyName = "Status",
                Context = position,
                Severity = ValidationResultSeverity.Error,
            });
        }
    }
}
