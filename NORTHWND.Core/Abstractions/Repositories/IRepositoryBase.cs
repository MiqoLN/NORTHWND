using System;
using System.Collections.Generic;

namespace NORTHWND.Core.Abstractions.Repositories
{
    public interface IRepositoryBase<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        IEnumerable<T> GetAll();
        T GetSingle(Func<T, bool> predicate);
        T Get(int id);
    }
}
