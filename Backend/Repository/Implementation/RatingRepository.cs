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
    public class RatingRepository : BaseRepository<Rating,long>, IRatingRepository
    {
        public IList<Rating> FindAllByEntityIdAndType(long id, EntityType type)
        {
            return Queryable().Where(r => r.EntityId == id && r.EntityType == type).ToList();
        }

        public IList<Rating> FindAllByEntityIdAndTypeAndUserId(long id, EntityType type, long userId)
        {
            return Queryable().Where(r => r.EntityId == id && r.EntityType == type && r.User.Id == userId).ToList();
        }
    }
}
