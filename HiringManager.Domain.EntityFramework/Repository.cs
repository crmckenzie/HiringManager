using System.Data.Entity;
using System.Linq;
using HiringManager.EntityModel;

namespace HiringManager.EntityFramework
{
    public class Repository : DbContext, IRepository
    {
        internal IDbSet<Candidate> Candidates { get; set; }
        internal IDbSet<CandidateStatus> CandidateStatuses { get; set; }
        internal IDbSet<ContactInfo> ContactInfo { get; set; }
        internal IDbSet<Document> Documents { get; set; }
        internal IDbSet<Manager> Managers { get; set; }
        internal IDbSet<Message> Messages { get; set; }
        internal IDbSet<Position> Positions { get; set; }

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