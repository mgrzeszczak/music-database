using Common.Model;
using Common.Scope;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Repository
{
    public abstract class BaseRepository<T, ID> : IBaseRepository<T,ID> where T : Entity<ID>
    {
        protected ISession GetSession()
        {
            return Context.ProvideSession();
        }

        public T FindOne(ID id)
        {
            return GetSession().Load<T>(id);
        }

        public IList<T> FindAll()
        {
            return GetSession().CreateCriteria(typeof(T)).List<T>();            
        }

        public T Save(T entity)
        {
            GetSession().Save(entity);
            return entity;
        }

        public void Update(T entity)
        {
            GetSession().Update(entity);
        }

        public void Delete(T entity)
        {
            GetSession().Delete(entity);
        }

        protected IQueryable<T> Queryable()
        {
            return GetSession().QueryOver<T>().List<T>().AsQueryable<T>();
        }

        public IList<T> Page<TKey>(int pageNr,int amountPerPage, Func<T,TKey> keyMapper, IComparer<TKey> comparator)
        {
            return Queryable().OrderBy(keyMapper, comparator).Skip((pageNr - 1) * amountPerPage).Take(amountPerPage).ToList();
        }

    }

}
