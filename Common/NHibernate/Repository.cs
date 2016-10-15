using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.NHibernate
{
    public abstract class Repository<T, ID>
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

    }

}
