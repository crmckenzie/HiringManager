using System.Linq;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.Web.Infrastructure.AutoMapper;
using HiringManager.Web.ViewModels.Positions;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.AutoMapperProfile
{
    [TestFixture]
    public class AddCandidateViewModelMapperTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
            AutoMapperConfiguration.Configure();
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
        }

        [Test]
        public void Map()
        {
            // Arrange
            var viewModel = Builder<AddCandidateViewModel>
                .CreateNew()
                .Build()
                ;

            // Act
            var result = global::AutoMapper.Mapper.Map<AddCandidateRequest>(viewModel);

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
