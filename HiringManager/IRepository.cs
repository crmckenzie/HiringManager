using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringManager
{
    public interface IRepository
    {
        IQueryable<T> Query<T>() where T: class;
        void Store<T>(T item) where T: class;
        void Delete<T>(T item) where T:class;
        void Commit();
        T Get<T>(int key) where T:class;
    }
}
