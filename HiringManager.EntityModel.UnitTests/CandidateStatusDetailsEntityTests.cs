using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.EntityModel.UnitTests
{
    [TestFixture]
    public class CandidateStatusEntityTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Position = Substitute.For<Position>();
            this.Position.Openings = new List<Opening>();

            this.Candidate = Builder<Candidate>.CreateNew().Build();

            this.CandidateStatus = Builder<CandidateStatus>
                .CreateNew()
                .Do(row => row.Position = this.Position)
                .Do(row => row.Candidate = this.Candidate)
                .Build()
                ;
        }

        public Candidate Candidate { get; set; }

        public CandidateStatus CandidateStatus { get; set; }

        public Position Position { get; set; }

        [Test]
        [TestCase(false, "Resume Received", true)]
        [TestCase(true, "Resume Received", false)]
        [TestCase(false, "Hired", false)]
        [TestCase(true, "Hired", false)]
        public void CanHire(bool isFilled, string status, bool expected)
        {
            // Arrange
            this.Position.IsFilled().Returns(isFilled);
            this.CandidateStatus.Status = status;

            if (status == "Hired")
            {
                this.Position.Openings.Add(new Opening()
                                           {
                                               FilledBy = this.CandidateStatus.Candidate
                                           });
            }

            // Act

            // Assert
            Assert.That(this.CandidateStatus.CanHire(), Is.EqualTo(expected));
        }

        [Test]
        [TestCase(false, "Resume Received", true)]
        [TestCase(true, "Resume Received", false)]
        [TestCase(false, "Passed", false)]
        [TestCase(true, "Passed", false)]
        [TestCase(true, "Hired", false)]
        [TestCase(false, "Hired", false)]
        public void CanPass(bool isFilled, string status, bool expected)
        {
            // Arrange
            this.Position.IsFilled().Returns(isFilled);
            this.CandidateStatus.Status = status;

            if (status == "Hired")
            {
                this.Position.Openings.Add(new Opening()
                {
                    FilledBy = this.CandidateStatus.Candidate
                });
            }

            // Act

            // Assert
            Assert.That(this.CandidateStatus.CanPass(), Is.EqualTo(expected));
        }

        [Test]
        [TestCase(false, "Resume Received", true)]
        [TestCase(true, "Resume Received", false)]
        [TestCase(false, "Passed", false)]
        [TestCase(true, "Passed", false)]
        [TestCase(true, "Hired", false)]
        [TestCase(false, "Hired", false)]
        public void CanSetStatus(bool isFilled, string status, bool expected)
        {
            // Arrange
            this.Position.IsFilled().Returns(isFilled);
            this.CandidateStatus.Status = status;

            if (status == "Hired")
            {
                this.Position.Openings.Add(new Opening()
                {
                    FilledBy = this.CandidateStatus.Candidate
                });
            }

            // Act

            // Assert
            Assert.That(this.CandidateStatus.CanSetStatus(), Is.EqualTo(expected));
        }



    }
}
