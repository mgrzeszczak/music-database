using Backend.Repository;
using Common.Domain;
using Common.Model;
using System;
using System.Collections.Generic;

namespace Backend.Service
{
    public interface IBaseService<T,ID,R> where T : Entity<ID> where R: IBaseRepository<T,ID>
    {
        T Save(T t);
        void Delete(ID id);
        T Update(T t);
        T FindById(ID id);
        Page<T> SearchBy(string searchText, int pageNr, int amountPerPage);
        Page<T> SearchBy<TKey>(string searchText, Func<T, TKey> keyMapper, IComparer<TKey> comparator, int pageNr, int amountPerPage);
    }
}
