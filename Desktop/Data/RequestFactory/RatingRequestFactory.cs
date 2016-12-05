using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Common.Model.Enum;
using RestSharp;

namespace Desktop.Data
{
    class RatingRequestFactory : IRatingRequestFactory
    {
        public IRestRequest AddRatingRequest(Rating rating)
        {
            throw new NotImplementedException();
        }

        public IRestRequest UpdateRatingRequest(Rating rating)
        {
            throw new NotImplementedException();
        }

        public IRestRequest AverageRatingRequest(EntityType entityType, long entityId)
        {
            throw new NotImplementedException();
        }

        public IRestRequest GetRatingRequest(EntityType entityType, long entityId, long userId)
        {
            throw new NotImplementedException();
        }
    }
}
