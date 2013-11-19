using System.Data.Entity;
using FizzWare.NBuilder;
using HiringManager.EntityFramework;
using HiringManager.EntityModel;
using NUnit.Framework;

namespace HiringManager.Domain.EntityFramework.IntegrationTests
{
    [TestFixture]
    public class HiringManagerTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<Repository>());
        }

        [Test]
        public void Insert()
        {
            using (var repository = new Repository())
            {
                // Arrange
                var hiringManager = Builder<Manager>
                    .CreateNew()
                    .Build()
                    ;

                // Act
                repository.Store(hiringManager);
                repository.Commit();

                // Assert
                Assert.That(hiringManager.ManagerId, Is.Not.Null);

            }
        }

        [Test]
        public void Get()
        {
            int? managerId = null;
            using (var repository = new Repository())
            {
                // Arrange
                var hiringManager = Builder<Manager>
                    .CreateNew()
                    .Build()
                    ;

                // Act
                repository.Store(hiringManager);
                repository.Commit();

                // Assert
                managerId = hiringManager.ManagerId;
            }

            // Act
            using (var repository = new Repository())
            {
                var manager = repository.Get<Manager>(managerId.Value);
                Assert.That(manager, Is.Not.Null);
            }

        }

        [Test]
        public void AddCandidateUsingReferences()
        {
            using (var repository = new Repository())
            {
                var hiringManager = Builder<Manager>
                    .CreateNew()
                    .Build()
                    ;

                var position = Builder<Position>
                    .CreateNew()
                    .Do(row => row.CreatedBy = hiringManager)
                    .Build()
                    ;

                var candidate = Builder<Candidate>
                    .CreateNew()
                    .Build()
                    ;

                var candidateStatus = new CandidateStatus()
                                      {
                                          Candidate = candidate,
                                          Position = position,
                                          Status = "Resume Received"
                                      };

                repository.Store(hiringManager);
                repository.Store(position);
                repository.Store(candidate);
                repository.Store(candidateStatus);

                repository.Commit();


            }

        }

        [Test]
        public void AddCandidateUsingIds()
        {
            using (var repository = new Repository())
            {
                var hiringManager = Builder<Manager>
                    .CreateNew()
                    .Build()
                    ;

                var position = Builder<Position>
                    .CreateNew()
                    .Do(row => row.CreatedBy = hiringManager)
                    .Build()
                    ;

                var candidate = Builder<Candidate>
                    .CreateNew()
                    .Build()
                    ;

                var candidateStatus = new CandidateStatus()
                {
                    CandidateId = candidate.CandidateId,
                    PositionId = position.PositionId,
                    Status = "Resume Received"
                };

                repository.Store(hiringManager);
                repository.Store(position);
                repository.Store(candidate);
                repository.Store(candidateStatus);

                repository.Commit();


            }
        }
    }
}
