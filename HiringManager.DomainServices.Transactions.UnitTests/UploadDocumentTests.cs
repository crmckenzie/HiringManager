using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using HiringManager.DomainServices.Candidates;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class UploadDocumentTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.UnitOfWork = Substitute.For<IUnitOfWork>();
            this.UploadService = Substitute.For<IUploadService>();
            this.Transaction = new UploadDocument(this.UnitOfWork, this.UploadService);
        }

        public IUploadService UploadService { get; set; }

        public IUnitOfWork UnitOfWork { get; set; }

        public UploadDocument Transaction { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            const string someFileName = "some file name";

            const int documentId = 13241234;
            var request = new UploadDocumentRequest()
                          {
                              CandidateId = 12341234,
                              Document = new MemoryStream(),
                              FileName = Guid.NewGuid().ToString(),
                          };

            this.UploadService.Save(request.Document).Returns(someFileName);
            this.UnitOfWork.NewDbContext().When(r => r.Add(Arg.Any<Document>()))
                .Do(ci => ci.Arg<Document>().DocumentId = documentId);

            // Act
            var response = this.Transaction.Execute(request);

            // Assert
            this.UploadService.Received().Save(request.Document);
            this.UnitOfWork.NewDbContext()
                .Received()
                .Add(Arg.Is<Document>(
                        d =>
                            d.DisplayName == request.FileName && d.DocumentId == documentId &&
                            d.CandidateId == request.CandidateId && d.FileName == someFileName));

            this.UnitOfWork.NewDbContext().Received().SaveChanges();
            this.UnitOfWork.NewDbContext().Received().Dispose();

            Assert.That(response, Is.Not.Null);
            Assert.That(response.DocumentId, Is.EqualTo(documentId));
            Assert.That(response.Title, Is.EqualTo(request.FileName));
        }

    }
}
