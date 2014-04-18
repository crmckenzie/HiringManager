using System;
using System.Linq;

namespace HiringManager
{
    public interface IRepository : IDisposable
    {
        IQueryable<T> Query<T>() where T : class;
        void Store<T>(T item) where T : class;
        void Delete<T>(T item) where T : class;
        void Commit();
        T Get<T>(int key) where T : class;
    }
}
