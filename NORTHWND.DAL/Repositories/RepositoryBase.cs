using NORTHWND.Core.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NORTHWND.DAL.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        protected readonly NORTHWNDContext Context;
        public RepositoryBase(NORTHWNDContext context)
        {
            Context = context;
        }
        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        public T Get(int id)
        {
            return Context.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return Context.Set<T>().ToArray();
        }

        public T GetSingle(Func<T, bool> predicate)
        {
            return Context.Set<T>().FirstOrDefault(predicate);
        }

        public void Remove(T entity)
        {
            Context.Set<T>().Remove(entity);
        }
        public void RemoveRange(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Remove(entity);
            }
        }
        public void Update(T entity)
        {
            Context.Set<T>().Update(entity);
        }
    }
}
