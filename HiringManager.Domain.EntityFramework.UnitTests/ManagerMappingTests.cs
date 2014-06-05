using System.Data.Entity;
using FizzWare.NBuilder;
using HiringManager.EntityFramework;
using HiringManager.EntityModel;
using NUnit.Framework;

namespace HiringManager.Domain.EntityFramework.IntegrationTests
{
    [TestFixture]
    public class ManagerMappingTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
            IntegrationTestHelpers.IntegrationTestConfiguration.Configure();
            //            Database.SetInitializer(new DropCreateDatabaseAlways<HiringManagerDbContext>());
        }

        [Test]
        public void Insert()
        {
            using (var repository = new HiringManagerDbContext())
            {
                // Arrange
                var hiringManager = Builder<Manager>
                    .CreateNew()
                    .Build()
                    ;

                // Act
                repository.Add(hiringManager);
                repository.SaveChanges();

                // Assert
                Assert.That(hiringManager.ManagerId, Is.Not.Null);

            }
        }

        [Test]
        public void Get()
        {
            int? managerId = null;
            using (var repository = new HiringManagerDbContext())
            {
                // Arrange
                var hiringManager = Builder<Manager>
                    .CreateNew()
                    .Build()
                    ;

                // Act
                repository.Add(hiringManager);
                repository.SaveChanges();

                // Assert
                managerId = hiringManager.ManagerId;
            }

            // Act
            using (var repository = new HiringManagerDbContext())
            {
                var manager = repository.Get<Manager>(managerId.Value);
                Assert.That(manager, Is.Not.Null);
            }

        }

        [Test]
        public void AddCandidateUsingReferences()
        {
            using (var repository = new HiringManagerDbContext())
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

                repository.Add(hiringManager);
                repository.Add(position);
                repository.Add(candidate);
                repository.Add(candidateStatus);

                repository.SaveChanges();


            }

        }

        [Test]
        public void AddCandidateUsingIds()
        {
            using (var repository = new HiringManagerDbContext())
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

                repository.Add(hiringManager);
                repository.Add(position);
                repository.Add(candidate);
                repository.Add(candidateStatus);

                repository.SaveChanges();


            }
        }
    }
}
