using Common.Model;
using System;
using System.Collections.Generic;

namespace Backend.Repository
{
    public interface IBaseRepository<T,ID> where T : Entity<ID>
    {
        T FindOne(ID id);
        IList<T> FindAll();
        T Save(T entity);
        void Update(T entity);
        void Delete(T entity);
        IList<T> Page<TKey>(int pageNr, int amountPerPage, Func<T, TKey> keyMapper, IComparer<TKey> comparator);
    }
}
