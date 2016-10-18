using Backend.Repository;
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

        public void Delete(T t)
        {
            this.repository.Delete(t);
        }

        public T FindById(ID id)
        {
            return this.repository.FindOne(id);
        }

        public T Save(T t)
        {
            return this.repository.Save(t);
        }

        public abstract IList<T> SearchBy(string searchText, int pageNr, int amountPerPage);

        public void Update(T t)
        {
            this.repository.Update(t);
        }
    }
}
