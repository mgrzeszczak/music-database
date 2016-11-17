using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Repository.Interface;
using Common.Model;
using Common.Model.Enum;

namespace Backend.Service.Interface
{
    public interface IRatingService : IBaseService<Rating, long, IRatingRepository>
    {
        double GetAverageRating(EntityType type, long id);
        Rating GetUserRatingForEntity(EntityType type, long entityId, long userId);
    }
}
