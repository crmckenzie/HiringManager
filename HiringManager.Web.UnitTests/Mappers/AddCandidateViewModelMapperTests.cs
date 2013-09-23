using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.Web.Mappers.Positions;
using HiringManager.Web.Models.Positions;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.Mappers
{
    [TestFixture]
    public class AddCandidateViewModelMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Mapper = new AddCandidateViewModelMapper();
        }

        public AddCandidateViewModelMapper Mapper { get; set; }

        [Test]
        public void Map()
        {
            // Arrange
            var viewModel = Builder<AddCandidateViewModel>
                .CreateNew()
                .Build()
                ;

            // Act
            var result = this.Mapper.Map(viewModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.CandidateName, Is.EqualTo(viewModel.Name));
            Assert.That(result.PositionId, Is.EqualTo(viewModel.PositionId));

            var email = result.ContactInfo.SingleOrDefault(row => row.Type == "Email");
            var phone = result.ContactInfo.SingleOrDefault(row => row.Type == "Phone");

            Assert.That(email.Value, Is.EqualTo(viewModel.EmailAddress));
            Assert.That(phone.Value, Is.EqualTo(viewModel.PhoneNumber));
        }

    }
}
