using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FizzWare.NBuilder;
using HiringManager.EntityFramework;
using HiringManager.EntityModel;
using IntegrationTestHelpers;
using NUnit.Framework;

namespace HiringManager.Domain.EntityFramework.IntegrationTests
{
    [TestFixture]
    public class CandidateStatusMappingTests
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

                db.Update(manager)
                    .Update(source)
                    .Update(candidate)
                    .Update(position)
                    .Update(position.Candidates.Single())
                    .SaveChanges()
                    ;
                //db.Sources.Add(source);
                //db.Candidates.Add(candidate);
                //db.Positions.Add(position);
                //db.CandidateStatuses.Add(position.Candidates.Single());

                // Act
                //db.SaveChanges();

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

    }
}
