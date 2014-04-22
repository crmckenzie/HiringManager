using System;
using System.Linq;

namespace HiringManager
{
    public interface IDbContext : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
        IDbContext Add<T>(T item) where T : class;
        IDbContext Update<T>(T item) where T : class;
        IDbContext AddOrUpdate<T>(T item, object key) where T : class;
        IDbContext Delete<T>(T item) where T : class;
        void SaveChanges();
        T Get<T>(int key) where T : class;
    }
}
