using System.Collections.Generic;
using System.Linq;
using FizzWare.NBuilder;
using HiringManager.Domain;
using HiringManager.Mappers;
using HiringManager.Specifications;
using Isg.Specification;
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

        }

        [SetUp]
        public void BeforeEachTestRuns()
        {
            this.Repository = Substitute.For<IRepository>();
            this.FluentMapper = Substitute.For<IFluentMapper>();
            this.Query = new QueryPositionSummaries(this.Repository, this.FluentMapper);
        }

        public IFluentMapper FluentMapper { get; set; }

        public IRepository Repository { get; set; }

        public QueryPositionSummaries Query { get; set; }

        [Test]
        public void Execute()
        {
            // Arrange
            var positions = Builder<Position>
                .CreateListOfSize(10)
                .Build()
                ;

            this.Repository.Query<Position>()
                .Returns(positions.AsQueryable())
                ;

            var request = new QueryPositionSummariesRequest();

            var specification = new AlwaysTrueSpecification<Position>();
            this.FluentMapper
                .Map<ISpecification<Position>>()
                .From<QueryPositionSummariesRequest>(request)
                .Returns(specification)
                ;

            var positionSummaries = new PositionSummary[10];
            this.FluentMapper
                .MapEnumerable<PositionSummary>()
                .FromEnumerable(Arg.Any<IEnumerable<Position>>())
                .Returns(positionSummaries.AsEnumerable())
                ;

            // Act
            var response = this.Query.Execute(request);

            // Assert
            Assert.That(response, Is.Not.Null);
            Assert.That(response.Page, Is.EqualTo(1));
            Assert.That(response.PageSize, Is.EqualTo(10));
            Assert.That(response.TotalRecords, Is.EqualTo(10));
            Assert.That(response.Data, Is.EquivalentTo(positionSummaries));
        }

    }
}
