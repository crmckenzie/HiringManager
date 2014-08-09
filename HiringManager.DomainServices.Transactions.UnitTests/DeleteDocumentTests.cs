using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class DeleteDocumentTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.UploadService = Substitute.For<IUploadService>();
            this.UnitOfWork = Substitute.For<IUnitOfWork>();
            this.DeleteDocument = new DeleteDocument(this.UnitOfWork, this.UploadService);
        }

        public IUploadService UploadService { get; set; }

        public IUnitOfWork UnitOfWork { get; set; }

        public DeleteDocument DeleteDocument { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            const int id = 321423;
            var theDocument = new Document()
                              {
                                  FileName = "some file name"
                              };
            this.UnitOfWork.NewDbContext().Get<Document>(id).Returns(theDocument);
            // Act

            var response = this.DeleteDocument.Execute(id);

            // Assert
            this.UnitOfWork.NewDbContext().Received().Get<Document>(id);
            this.UnitOfWork.NewDbContext().Received().Delete(theDocument);
            this.UnitOfWork.NewDbContext().Received().SaveChanges();
            this.UnitOfWork.NewDbContext().Received().Dispose();

            this.UploadService.Received().Delete(theDocument.FileName);
        }

    }
}
