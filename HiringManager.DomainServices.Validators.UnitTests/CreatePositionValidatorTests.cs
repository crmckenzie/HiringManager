using System;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Simple.Validation;

namespace HiringManager.DomainServices.Validators.UnitTests
{
    [TestFixture]
    public class CreatePositionValidatorTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Validator = new CreatePositionValidator();
        }

        public CreatePositionValidator Validator { get; set; }

        [Test]
        [TestCase(0, false)]
        [TestCase(1, true)]
        [TestCase(10, true)]
        public void Validate(int openings, bool isValid)
        {
            // Arrange
            var request = new CreatePositionRequest()
                          {
                              Openings = openings,
                          };

            // Act
            var results = this.Validator.Validate(request);

            // Assert
            var template = new ValidationResult()
            {
                PropertyName = "Openings",
            };

            if (isValid)
            {
                results.AssertIsValidFor(template);
            }
            else
            {
                results.AssertInvalidFor(template);
            }
        }
    }
}
