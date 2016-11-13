using Common.Model;
using Backend.Scope;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using Common.Domain;

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

        protected Page<T> Page<TKey>(int pageNr, int amountPerPage, Func<T, TKey> keyMapper, IComparer<TKey> comparator, IQueryable<T> queryable, int totalAmount, int totalPages)
        {
            //IList<T> items = queryable.OrderBy(keyMapper, comparator).Skip((pageNr - 1) * amountPerPage).ToList();
            //items = items.Take(amountPerPage).ToList();
            IList<T> items = queryable.OrderBy(keyMapper, comparator).Skip((pageNr - 1) * amountPerPage).Take(amountPerPage).ToList();
            return new Page<T>(items, totalAmount, pageNr, amountPerPage, totalPages);
        }

        public Page<T> SearchBy(string searchText, Func<T, string> searchParameterMapper, int pageNr, int amountPerPage)
        {
            /*var result = Queryable().Where(t => searchParameterMapper(t).ToUpper().Contains(searchText.ToUpper()));
            int count = result.Count();
            int pageCount = count / amountPerPage;
            if (count % amountPerPage != 0) pageCount += 1;
            return Page(pageNr,amountPerPage,searchParameterMapper,StringComparer.Ordinal, result, count,pageCount);*/
            return SearchBy(searchText, searchParameterMapper, searchParameterMapper, StringComparer.Ordinal, pageNr, amountPerPage);
        }

        public Page<T> SearchBy<TKey>(string searchText, Func<T, string> searchParameterMapper, Func<T, TKey> keyMapper, IComparer<TKey> comparator, int pageNr, int amountPerPage)
        {
            var result = Queryable().Where(t => searchParameterMapper(t).ToUpper().Contains(searchText.ToUpper()));
            int count = result.Count();
            int pageCount = count / amountPerPage;
            if (count % amountPerPage != 0) pageCount += 1;
            return Page(pageNr, amountPerPage, keyMapper,comparator, result, count, pageCount);
        }
    }

}
