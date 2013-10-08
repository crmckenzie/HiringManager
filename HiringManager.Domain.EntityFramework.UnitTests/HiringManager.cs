using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.EntityFramework;
using HiringManager.EntityModel;
using NUnit.Framework;

namespace HiringManager.Domain.EntityFramework.UnitTests
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
    }
}
