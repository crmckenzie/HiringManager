using System.IO;
using FizzWare.NBuilder;
using HiringManager.DomainServices.AutoMapperProfiles;
using HiringManager.DomainServices.Positions;
using HiringManager.EntityModel;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class AddNewCandidateToPositionTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.AddProfile<DomainProfile>();
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Db = Substitute.For<IDbContext>();
            this.UploadService = Substitute.For<IUploadService>();
            this.AddNewCandidateToPosition = new AddNewCandidateToPosition(this.Db, this.UploadService);
        }

        public IUploadService UploadService { get; set; }

        public IDbContext Db { get; set; }

        public AddNewCandidateToPosition AddNewCandidateToPosition { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            var request = Builder<NewCandidateRequest>
                .CreateNew()
                .Do(row => row.ContactInfo = Builder<ContactInfoDetails>.CreateListOfSize(2).Build())
                .Build()
                ;

            const int candidateStatusId = 3;
            const int candidateId = 4;

            this.Db.When(r => r.Add(Arg.Any<Candidate>()))
                .Do(arg => arg.Arg<Candidate>().CandidateId = candidateId);

            this.Db.When(r => r.Add(Arg.Any<CandidateStatus>()))
                .Do(arg =>
                    {
                        arg.Arg<CandidateStatus>().CandidateStatusId = candidateStatusId;
                        arg.Arg<CandidateStatus>().CandidateId = candidateId;
                    });

            // Act
            var response = this.AddNewCandidateToPosition.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);

            this.Db.Received()
                .Add(Arg.Is<Candidate>(arg => arg.Name == request.CandidateName && arg.SourceId == request.SourceId));

            foreach (var contactInfo in request.ContactInfo)
            {
                this.Db.Received()
                    .Add(Arg.Is<ContactInfo>(arg => arg.Type == contactInfo.Type && arg.Value == contactInfo.Value && arg.Candidate != null));

            }

            this.Db
                .Received()
                .Add(Arg.Is<CandidateStatus>(arg => arg.PositionId == request.PositionId && arg.Status == "Resume Received"))
                ;

            this.Db
                .Received()
                .Add(Arg.Is<Candidate>(arg => arg.Name == request.CandidateName))
                ;

            Assert.That(response.PositionId, Is.EqualTo(request.PositionId));
            Assert.That(response.CandidateStatusId, Is.EqualTo(candidateStatusId));
            Assert.That(response.CandidateId, Is.EqualTo(candidateId));
        }

        [Test]
        public void Execute_WithDocuments()
        {
            // Arrange
            var document1 = new { Name = "Resume 1", Stream = new MemoryStream() };
            var document2 = new { Name = "Resume 2", Stream = new MemoryStream() };
            var documents = new[]
                            {
                                document1, 
                                document2, 
                            };

            var request = Builder<NewCandidateRequest>
                .CreateNew()
                .Do(row => row.ContactInfo = Builder<ContactInfoDetails>.CreateListOfSize(2).Build())
                .Do(row =>
                {
                    foreach (var document in documents)
                    {
                        row.Documents.Add(document.Name, document.Stream);
                    }
                })
                .Build()
                ;

            const int candidateStatusId = 3;
            const int candidateId = 4;

            this.Db.When(r => r.Add(Arg.Any<Candidate>()))
                .Do(arg => arg.Arg<Candidate>().CandidateId = candidateId);

            this.Db.When(r => r.Add(Arg.Any<CandidateStatus>()))
                .Do(arg =>
                {
                    arg.Arg<CandidateStatus>().CandidateStatusId = candidateStatusId;
                    arg.Arg<CandidateStatus>().CandidateId = candidateId;
                });

            const string fileName1 = "filename1";
            const string fileName2 = "filename2";
            this.UploadService.Save(document1.Stream).Returns(fileName1);
            this.UploadService.Save(document2.Stream).Returns(fileName2);

            // Act
            var response = this.AddNewCandidateToPosition.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);

            foreach (var document in documents)
            {
                this.UploadService.Received().Save(document.Stream);
            }

            this.Db.Received()
                .Add(Arg.Is<Document>(
                        d => d.FileName == fileName1 && d.DisplayName == document1.Name && d.CandidateId == candidateId));

            this.Db.Received()
                .Add(Arg.Is<Document>(
                        d => d.FileName == fileName2 && d.DisplayName == document2.Name && d.CandidateId == candidateId));

        }

    }
}
