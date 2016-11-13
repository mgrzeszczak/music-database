using Backend.Repository;
using Common.Domain;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public abstract class BaseService<T, ID, R> : IBaseService<T, ID,R> where T : Entity<ID> where R : IBaseRepository<T, ID>
    {
        protected R repository;

        public BaseService(R repository) {
            this.repository = repository;
        }

        public virtual void Delete(ID id)
        {
            T t = this.repository.FindOne(id);
            if (t == null) return;
            this.repository.Delete(t);
        }

        public T FindById(ID id)
        {
            T t = this.repository.FindOne(id);
            return t;
        }

        public T Save(T t)
        {
            return this.repository.Save(t);
        }

        public abstract Page<T> SearchBy(string searchText, int pageNr, int amountPerPage);

        public abstract Page<T> SearchBy<TKey>(string searchText, Func<T, TKey> keyMapper, IComparer<TKey> comparator, int pageNr, int amountPerPage);

        public virtual T Update(T t)
        {
            this.repository.Update(t);
            return t;
        }
    }
}
