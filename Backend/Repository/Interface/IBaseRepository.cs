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
        IList<T> SearchBy(string searchText, Func<T, string> searchParameterMapper,int pageNr, int amountPerPage);

    }
}
