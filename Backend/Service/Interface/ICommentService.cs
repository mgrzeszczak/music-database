using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Repository;
using Backend.Repository.Interface;
using Common.Domain;
using Common.Model;
using Common.Model.Enum;

namespace Backend.Service.Interface
{
    public interface ICommentService : IBaseService<Comment, long, ICommentRepository>
    {
        Page<Comment> PageByEntityIdAndType(long id, EntityType type, int pageNr, int amountPerPage);
        Page<Comment> PageByEntityIdAndTypeAndUserId(long id, EntityType type, long userId, int pageNr, int amountPerPage);
    }
}
