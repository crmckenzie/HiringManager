using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using HiringManager.EntityModel;

namespace HiringManager.EntityFramework
{
    public class HiringManagerDbContext : System.Data.Entity.DbContext, IDbContext
    {
        public IDbSet<Candidate> Candidates { get; set; }
        public IDbSet<CandidateStatus> CandidateStatuses { get; set; }
        public IDbSet<ContactInfo> ContactInfo { get; set; }
        public IDbSet<Manager> Managers { get; set; }
        public IDbSet<Position> Positions { get; set; }

        public IQueryable<T> Query<T>() where T : class
        {
            return base.Set<T>();
        }

        public void Add<T>(T item) where T : class
        {
            base.Set<T>().Add(item);
        }

        protected ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter)this).ObjectContext; }
        }

        object GetPrimaryKeyValue(DbEntityEntry entry)
        {
            var objectStateEntry = this.ObjectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity);
            return objectStateEntry.EntityKey.EntityKeyValues[0].Value;
        }

        //public void Save<T>(T item) where T : class
        //{
        //    var entry = base.Entry(item);
        //    if (entry.State == EntityState.Detached)
        //    {
        //        base.Set<T>().Attach(item);
        //        entry = base.Entry(item);

        //        var key = GetPrimaryKeyValue(entry);
        //        if (key == null)
        //        {
        //            entry.State = EntityState.Added;
        //        }
        //        else
        //        {
        //            entry.State = EntityState.Modified;
        //        }

        //    }
        //}

        public void Delete<T>(T item) where T : class
        {
            base.Set<T>().Remove(item);
        }

        public new void SaveChanges()
        {
            try
            {
                base.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbeve)
            {
                Trace.Indent();

                foreach (var dbEntityValidationResult in dbeve.EntityValidationErrors)
                {
                    var typeName = dbEntityValidationResult.Entry.Entity.GetType().Name;
                    var message = string.Format("Error saving {0}; {1}", typeName, dbEntityValidationResult.Entry.State.ToString());
                    Trace.WriteLine(message);
                    foreach (var dbValidationError in dbEntityValidationResult.ValidationErrors)
                    {
                        var errorMessage = dbValidationError.PropertyName + "; " + dbValidationError.ErrorMessage;
                        Trace.WriteLine(errorMessage);
                    }
                }
                Trace.Flush();

                Trace.Unindent();
                throw;
            }
        }

        public T Get<T>(int key) where T : class
        {
            var result = base.Set<T>().Find(key);
            return result;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Candidate>()
                .HasMany(row => row.AppliedTo)
                .WithRequired(row => row.Candidate)
                ;
            modelBuilder.Entity<CandidateStatus>();
            modelBuilder.Entity<ContactInfo>();
            modelBuilder.Entity<Manager>()
                ;



            modelBuilder.Entity<Position>()
                .HasMany(row => row.Candidates)
                .WithRequired(row => row.Position)
                ;

            modelBuilder.Entity<Position>()
                .HasRequired(row => row.CreatedBy)
                .WithMany(row => row.Positions)
                ;

            modelBuilder.Entity<Position>()
                .HasMany(row => row.Openings)
                .WithRequired(row => row.Position)
                ;

            modelBuilder.Entity<Opening>()
                .HasRequired(m => m.Position)
                ;

            modelBuilder.Entity<Opening>()
                .HasOptional(m => m.FilledBy)
                ;

        }

        public System.Data.Entity.DbSet<HiringManager.EntityModel.Source> Sources { get; set; }


    }
}