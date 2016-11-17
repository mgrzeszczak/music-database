using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Repository.Interface;
using Common.Domain;
using Common.Model;
using Common.Model.Enum;

namespace Backend.Repository.Implementation
{
    public class RatingRepository : BaseRepository<Rating,long>, IRatingRepository
    {
        public double GetAverageRating(EntityType type, long id)
        {
            var result = Queryable().Where(r => r.EntityType == type && r.EntityId == id);
            var count = result.Count();
            var sum = result.Aggregate(0.0, (s, r) => s + r.Value);
            return count > 0 ? sum/count : 0;
        }

        public Rating GetUserRatingForEntity(EntityType type, long entityId, long userId)
        {
            return Queryable().FirstOrDefault(r => r.EntityType == type && r.EntityId == entityId && r.User.Id == userId);
        }
    }
}
