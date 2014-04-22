using System.Linq;
using FizzWare.NBuilder;
using HiringManager.EntityFramework;
using HiringManager.EntityModel;
using IntegrationTestHelpers;
using NUnit.Framework;

namespace HiringManager.Domain.EntityFramework.IntegrationTests
{
    [TestFixture]
    public class HiringManagerDbContextTests
    {
        [TestFixtureSetUp]
        public void BeforeAnyTestRuns()
        {
            IntegrationTestConfiguration.Configure();
        }

        [SetUp]
        public void BeforeEachTestRuns()
        {

        }

        [Test]
        public void Insert()
        {
            // Arrange
            int? positionId = null;
            using (var db = new HiringManagerDbContext())
            {

                var manager = Builder<Manager>
                    .CreateNew()
                    .Build()

                    ;
                var source = Builder<Source>
                    .CreateNew()
                    .Build()
                    ;

                var position = Builder<Position>
                    .CreateNew()
                    .Build();


                var candidate = Builder<Candidate>
                    .CreateNew()
                    .Build()
                    ;

                position.CreatedBy = manager;
                candidate.Source = source;
                position.Add(candidate);

                db
                    .Add(manager)
                    .Add(source)
                    .Add(candidate)
                    .Add(position)
                    .Add(position.Candidates.Single())
                    .SaveChanges()
                    ;

                positionId = position.PositionId;

            }


            // Assert
            using (var db = new HiringManagerDbContext())
            {
                var position = db.Get<Position>(positionId.Value);
                var candidateStatus = position.Candidates.Single();
                Assert.That(candidateStatus, Is.Not.Null);
                Assert.That(candidateStatus.Candidate, Is.Not.Null);
                Assert.That(candidateStatus.Candidate.Source, Is.Not.Null);
            }
        }

        [Test]
        public void Retrieve_Then_Update()
        {
            // Arrange
            int? managerId = null;
            using (var db = new HiringManagerDbContext())
            {

                var manager = Builder<Manager>
                    .CreateNew()
                    .Build()

                    ;

                db.Add(manager)
                    .SaveChanges()
                    ;

                managerId = manager.ManagerId;

            }


            // Act
            using (var db = new HiringManagerDbContext())
            {
                var inserted = db.Get<Manager>(managerId.Value);
                inserted.Name = "Updated";
                db.SaveChanges();
            }

            // Assert
            using (var db = new HiringManagerDbContext())
            {
                var updated = db.Get<Manager>(managerId.Value);
                Assert.That(updated.Name == "Updated");
            }

        }

        [Test]
        public void Retrieve_Then_Delete()
        {
            // Arrange
            int? managerId = null;
            using (var db = new HiringManagerDbContext())
            {

                var manager = Builder<Manager>
                    .CreateNew()
                    .Build()

                    ;

                db.Add(manager)
                    .SaveChanges()
                    ;

                managerId = manager.ManagerId;

            }


            // Act
            using (var db = new HiringManagerDbContext())
            {
                var inserted = db.Get<Manager>(managerId.Value);
                db.Delete(inserted);
                db.SaveChanges();
            }

            // Assert
            using (var db = new HiringManagerDbContext())
            {
                var deleted = db.Get<Manager>(managerId.Value);
                Assert.That(deleted, Is.Null);
            }

        }


        [Test]
        public void Detached_Updated()
        {
            // Arrange
            int? managerId = null;
            var manager = Builder<Manager>
                .CreateNew()
                .Build()
                ;
            using (var db = new HiringManagerDbContext())
            {
                db.Add(manager)
                    .SaveChanges()
                    ;

                managerId = manager.ManagerId;

            }


            // Act
            using (var db = new HiringManagerDbContext())
            {
                manager.Name = "Updated";
                db.Update(manager);
                db.SaveChanges();
            }

            // Assert
            using (var db = new HiringManagerDbContext())
            {
                var updated = db.Get<Manager>(managerId.Value);
                Assert.That(updated.Name, Is.EqualTo("Updated"));
            }

        }

        [Test]
        public void Detached_Deleted()
        {
            // Arrange
            int? managerId = null;
            var manager = Builder<Manager>
                .CreateNew()
                .Build()
                ;
            using (var db = new HiringManagerDbContext())
            {
                db.Add(manager)
                    .SaveChanges()
                    ;

                managerId = manager.ManagerId;

            }


            // Act
            using (var db = new HiringManagerDbContext())
            {
                manager.Name = "Updated";
                db.Delete(manager);
                db.SaveChanges();
            }

            // Assert
            using (var db = new HiringManagerDbContext())
            {
                var updated = db.Get<Manager>(managerId.Value);
                Assert.That(updated, Is.Null);
            }

        }
    }
}