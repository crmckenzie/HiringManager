using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.Web.Mappers.Positions;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.Mappers
{
    [TestFixture]
    public class CandidateStatusViewModelMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Mapper = new CandidateStatusViewModelMapper();
        }

        public CandidateStatusViewModelMapper Mapper { get; set; }

        [Test]
        public void Map()
        {
            // Arrange
            var details = Builder<CandidateStatusDetails>
                .CreateNew()
                .Build()
                ;

            // Act
            var viewModel = this.Mapper.Map(details);

            // Assert
            Assert.That(viewModel, Is.Not.Null);
            Assert.That(viewModel.CandidateId, Is.EqualTo(details.CandidateId));
            Assert.That(viewModel.CandidateName, Is.EqualTo(details.CandidateName));
            Assert.That(viewModel.CandidateStatusId, Is.EqualTo(details.CandidateStatusId));
            Assert.That(viewModel.PositionTitle, Is.EqualTo(details.PositionTitle));
            Assert.That(viewModel.PositionId, Is.EqualTo(details.PositionId));
            Assert.That(viewModel.Status, Is.EqualTo(details.Status));
        }

    }
}
