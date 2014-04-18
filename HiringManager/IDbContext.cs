using System;
using System.Linq;

namespace HiringManager
{
    public interface IDbContext : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
        void Add<T>(T item) where T : class;
        void Delete<T>(T item) where T : class;
        void SaveChanges();
        T Get<T>(int key) where T : class;
    }
}
