﻿using FizzWare.NBuilder;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;
using Simple.Validation;

namespace HiringManager.DomainServices.Validators.UnitTests
{
    [TestFixture]
    public class NewCandidateRequestValidatorTests
    {
        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.DbContext = Substitute.For<IDbContext>();
            this.Validator = new NewCandidateRequestValidator(this.DbContext);
        }

        public IDbContext DbContext { get; set; }

        public NewCandidateRequestValidator Validator { get; set; }

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
            var request = Builder<NewCandidateRequest>
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
            var request = Builder<NewCandidateRequest>
                .CreateNew()
                .Build()
                ;

            var position = Substitute.For<Position>();
            position.IsFilled().Returns(true);

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