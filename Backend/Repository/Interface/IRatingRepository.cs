using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain;
using Common.Model;
using Common.Model.Enum;

namespace Backend.Repository.Interface
{
    public interface IRatingRepository : IBaseRepository<Rating,long>
    {
        double GetAverageRating(EntityType type, long id);
        Rating GetUserRatingForEntity(EntityType type, long entityId, long userId);
    }
}
