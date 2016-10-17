using Common.Scope;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.NHibernate
{
    public abstract class Repository<T, ID> where T : class
    {
    
        protected ISession getSession()
        {
            return Context.provideSession();
        }

        public T findOne(ID id)
        {
            return getSession().Load<T>(id);
        }

        public IList<T> findAll()
        {
            return getSession().CreateCriteria(typeof(T)).List<T>();            
        }

        public T save(T entity)
        {
            getSession().Save(entity);
            return entity;
        }

        public void delete(T entity)
        {
            getSession().Delete(entity);
        }

        public IQueryable<T> query()
        {
            return getSession().QueryOver<T>().List<T>().AsQueryable<T>();
        }

        public void update(T entity)
        {
            getSession().Update(entity);
        }

        public IList<T> page<TKey>(int pageNr,int amountPerPage, Func<T,TKey> keyMapper, IComparer<TKey> comparator)
        {
            return query().OrderBy(keyMapper, comparator).Skip((pageNr - 1) * amountPerPage).Take(amountPerPage).ToList();
        }

    }

}
