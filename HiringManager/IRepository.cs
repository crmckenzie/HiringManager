using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringManager
{
    public interface IRepository
    {
        IQueryable<T> Query<T>();
        void Store<T>(T item);
        void Delete<T>(T item);
        void Commit();
        T Get<T>(int? key);
    }
}
