using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Model.Enum;

namespace Backend.Repository.Interface
{
    public interface IRatingRepository : IBaseRepository<Rating,long>
    {
        IList<Rating> FindAllByEntityIdAndType(long id, EntityType type);
        IList<Rating> FindAllByEntityIdAndTypeAndUserId(long id, EntityType type, long userId);
    }
}
