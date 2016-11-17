using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Repository.Interface;
using Common.Model;
using Common.Model.Enum;

namespace Backend.Repository.Implementation
{
    public class CommentRepository : BaseRepository<Comment, long>, ICommentRepository
    {
        public IList<Comment> FindAllByEntityIdAndType(long id, EntityType type)
        {
            return Queryable().Where(c => c.EntityId == id && c.EntityType == type).ToList();
        }

        public IList<Comment> FindAllByEntityIdAndTypeAndUserId(long id, EntityType type, long userId)
        {
            return Queryable().Where(c => c.EntityId == id && c.EntityType == type && c.User.Id == userId).ToList();
        }
    }
}
