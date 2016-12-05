using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Common.Domain;
using Common.Exception;
using NHibernate;
using NHibernate.Exceptions;

namespace WebApi.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            var e = context.Exception;
            if (e is BusinessException)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ExceptionResponse(e.Message,((BusinessException)e).Error));
            } else if (e is ObjectNotFoundException)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.NotFound,
                    new ExceptionResponse("Object not found",Error.NOT_FOUND));
            } else if (e is StaleObjectStateException)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest, 
                    new ExceptionResponse("Object has been changed", Error.INVALID_VERSION));
            } else if (e is GenericADOException)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new ExceptionResponse(e.Message,
                        e.InnerException != null ? RecognizeError(e.InnerException, e.Message) : Error.UNKNOWN_ERROR));
            } else context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest,
                    new ExceptionResponse("Unknown error", Error.UNKNOWN_ERROR));
        }

        private Error RecognizeError(Exception e, string message)
        {
            string innerMessage = e.Message;
            if (innerMessage.Contains("Duplicate"))
            {
                if (innerMessage.Contains("Name"))
                {
                    if (message.Contains("Model.Album"))
                    {
                        return Error.ALBUM_NAME_TAKEN;
                    }
                    if (message.Contains("Model.Artist"))
                    {
                        return Error.ARTIST_NAME_TAKEN;
                    }
                }
                if (innerMessage.Contains("Number"))
                {
                    if (message.Contains("Model.Album"))
                    {
                        return Error.ALBUM_NUMBER_TAKEN;
                    }
                    if (message.Contains("Model.Song"))
                    {
                        return Error.SONG_NUMBER_TAKEN;
                    }
                }
                if (innerMessage.Contains("Title"))
                {
                    return Error.SONG_TITLE_TAKEN;
                }
                if (innerMessage.Contains("Login"))
                {
                    return Error.LOGIN_TAKEN;
                }
                if (innerMessage.Contains("EntityType"))
                {
                    return Error.MULTIPLE_RATINGS;
                }
            }
            return Error.UNKNOWN_ERROR;
        }
    }

   
}