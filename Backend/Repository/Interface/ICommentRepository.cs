using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Model.Enum;

namespace Backend.Repository.Interface
{
    public interface ICommentRepository : IBaseRepository<Comment,long>
    {
        IList<Comment> FindAllByEntityIdAndType(long id, EntityType type);
        IList<Comment> FindAllByEntityIdAndTypeAndUserId(long id, EntityType type,long userId);
    }
}
