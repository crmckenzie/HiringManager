using System.Linq;
using FizzWare.NBuilder;
using HiringManager.EntityModel;
using HiringManager.Web.Infrastructure.AutoMapper;
using NSubstitute;
using NUnit.Framework;

namespace HiringManager.DomainServices.Transactions.UnitTests
{
    [TestFixture]
    public class QueryPositionSummariesTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
            AutoMapperConfiguration.Configure();
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Repository = Substitute.For<IRepository>();
            this.Query = new QueryPositionSummaries(this.Repository);
        }

        public IRepository Repository { get; set; }

        public QueryPositionSummaries Query { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            var positions = Builder<Position>
                .CreateListOfSize(10)
                .All()
                .Do(row => row.CreatedBy = Builder<Manager>.CreateNew().Build())
                .Do(row => row.FilledBy = Builder<Candidate>.CreateNew().Build())
                .Build()
                ;

            this.Repository.Query<Position>()
                .Returns(positions.AsQueryable())
                ;

            var request = new QueryPositionSummariesRequest();


            // Act
            var response = this.Query.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Page, Is.EqualTo(1));
            Assert.That(response.PageSize, Is.EqualTo(10));
            Assert.That(response.TotalRecords, Is.EqualTo(10));
            //            Assert.That(response.Data, Is.EquivalentTo(positionSummaries));
        }

    }
}
