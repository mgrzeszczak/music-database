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
    interface IRatingRequestFactory
    {

        IRestRequest AddRatingRequest(Rating rating);
        IRestRequest UpdateRatingRequest(Rating rating);
        IRestRequest AverageRatingRequest(EntityType entityType, long entityId);
        IRestRequest GetRatingRequest(EntityType entityType, long entityId, long userId);

    }
}
