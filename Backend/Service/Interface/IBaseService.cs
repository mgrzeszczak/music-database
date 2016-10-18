using Backend.Repository;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Service
{
    public interface IBaseService<T,ID,R> where T : Entity<ID> where R: IBaseRepository<T,ID>
    {
        T Save(T t);
        void Delete(T t);
        void Update(T t);
        T FindById(ID id);

    }
}
