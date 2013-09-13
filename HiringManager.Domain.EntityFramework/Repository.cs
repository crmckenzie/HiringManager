using System.Data.Entity;
using System.Linq;

namespace HiringManager.Domain.EntityFramework
{
    public class Repository : DbContext, IRepository
    {
        public IQueryable<T> Query<T>() where T: class
        {
            return base.Set<T>();
        }

        public void Store<T>(T item) where T:class
        {
            base.Set<T>().Add(item);
        }

        public void Delete<T>(T item) where T:class
        {
            base.Set<T>().Remove(item);
        }

        public void Commit()
        {
            base.SaveChanges();
        }

        public T Get<T>(int key) where T:class
        {
            var result =base.Set<T>().Find(key);
            return result;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidate>();
            modelBuilder.Entity<CandidateStatus>();
            modelBuilder.Entity<ContactInfo>();
            modelBuilder.Entity<Document>();
            modelBuilder.Entity<Manager>();
            modelBuilder.Entity<Message>();
            modelBuilder.Entity<Position>();
        }
    }
}