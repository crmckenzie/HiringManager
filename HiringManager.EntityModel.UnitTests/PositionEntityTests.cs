using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace HiringManager.EntityModel.UnitTests
{
    [TestFixture]
    public class PositionEntityTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
        }

        [Test]
        public void IsFilled_WhenThereAreNoOpenings()
        {
            // Arrange
            var position = new Position()
                           {

                           };

            // Act
            var result = position.IsFilled();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsFilled_WhenThereIsOneOpening_AndItIsNotFilled()
        {
            // Arrange
            var position = new Position()
            {
                Openings = { new Opening()}
            };

            // Act
            var result = position.IsFilled();

            // Assert
            Assert.That(result, Is.False);

        }

        [Test]
        public void IsFilled_WhenThereIsOneOpening_AndItIsFilled()
        {
            // Arrange
            var position = new Position()
            {
                Openings = { new Opening()
                             {
                                 FilledBy =new Candidate()
                             } }
            };

            // Act
            var result = position.IsFilled();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test]
        public void IsFilled_WhenThereAreTwoOpenings_AndOneIsFilled()
        {
            // Arrange
            var position = new Position()
            {
                Openings = { new Opening()
                             {
                                 FilledBy =new Candidate()
                             },
                             new Opening()
                             {
                                 
                             }
                }
            };

            // Act
            var result = position.IsFilled();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test]
        public void IsFilled_WhenThereAreTwoOpenings_AndBothAreFilled()
        {
            // Arrange
            var position = new Position()
            {
                Openings = { new Opening()
                             {
                                 FilledBy =new Candidate()
                             },
                             new Opening()
                             {
                                 FilledBy = new Candidate()
                             }
                }
            };

            // Act
            var result = position.IsFilled();

            // Assert
            Assert.That(result, Is.True);
        }
    }
}
