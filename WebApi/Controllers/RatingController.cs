using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Backend.Service.Interface;
using Common.Authorization;
using Common.Model;
using Common.Model.Enum;
using WebApi.Attributes;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [RoutePrefix("rating")]
    [@Authorize(Role.USER)]
    public class RatingController : ApiController
    {
        private IRatingService ratingService;

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        [Route("get")]
        [HttpGet]
        public Rating Get(EntityType entityType, long entityId)
        {
            return ratingService.GetUserRatingForEntity(entityType, entityId, AuthenticationUtils.CurrentAuthentication.GetUser().Id);
        }

        [Route("add")]
        [HttpPost]
        public Rating Add([FromBody, Valid] Rating rating)
        {
            rating.User = AuthenticationUtils.CurrentAuthentication.GetUser();
            return ratingService.Save(rating);
        }

        [Route("average")]
        [HttpGet]
        public double AverageRating(EntityType entityType, long entityId)
        {
            return ratingService.GetAverageRating(entityType, entityId);
        }

        [Route("update")]
        [HttpPut]
        public Rating Update([FromBody, Valid] Rating rating)
        {
            rating.User = AuthenticationUtils.CurrentAuthentication.GetUser();
            return ratingService.Update(rating);
        }
    }
}
