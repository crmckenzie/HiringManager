using System.Linq;
using System.Web;
using FizzWare.NBuilder;
using HiringManager.DomainServices;
using HiringManager.DomainServices.Positions;
using HiringManager.Web.Infrastructure.AutoMapper;
using HiringManager.Web.ViewModels.Positions;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.Web.UnitTests.AutoMapperProfile
{
    [TestFixture]
    public class NewCandidateViewModelMapperTests
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
            var file1 = Substitute.For<HttpPostedFileBase>();
            var file2 = Substitute.For<HttpPostedFileBase>();
            var documents = new[]
                            {
                                file1,
                                file2,
                            };
            file1.FileName.Returns("File 1");
            file2.FileName.Returns("File 2");

            var viewModel = Builder<NewCandidateViewModel>
                .CreateNew()
                .Do(row => row.Documents = documents)
                .Build()
                ;

            // Act
            var result = global::AutoMapper.Mapper.Map<NewCandidateRequest>(viewModel);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.CandidateName, Is.EqualTo(viewModel.Name));
            Assert.That(result.PositionId, Is.EqualTo(viewModel.PositionId));

            var email = result.ContactInfo.SingleOrDefault(row => row.Type == "Email");
            var phone = result.ContactInfo.SingleOrDefault(row => row.Type == "Phone");

            Assert.That(email.Value, Is.EqualTo(viewModel.EmailAddress));
            Assert.That(phone.Value, Is.EqualTo(viewModel.PhoneNumber));

            Assert.That(result.Documents.ContainsKey(file1.FileName));
            Assert.That(result.Documents.ContainsKey(file2.FileName));

            Assert.That(result.Documents[file1.FileName], Is.SameAs(file1.InputStream));
            Assert.That(result.Documents[file2.FileName], Is.SameAs(file2.InputStream));
        }

    }
}
