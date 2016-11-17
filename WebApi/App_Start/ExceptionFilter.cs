using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;
using Common.Domain;
using Common.Exception;

namespace WebApi.App_Start
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is BusinessException)
            {
                context.Response = context.Request.CreateResponse(HttpStatusCode.BadRequest,new ExceptionResponse(context.Exception.Message,((BusinessException)context.Exception).Error));
            }
            else context.Response = context.Request.CreateResponse(HttpStatusCode.InternalServerError, new ExceptionResponse(context.Exception.Message,Error.UNKNOWN_ERROR));
        }

        private Error RecognizeException()
        {
            return Error.UNKNOWN_ERROR;
        }
    }
    public class ExceptionResponse
    {
        public DateTime TimeStamp { get; }
        public string Message { get; }
        public Error ErrorCode { get; }

        public ExceptionResponse(string message, Error errorCode)
        {
            Message = message;
            ErrorCode = errorCode;
            TimeStamp = DateTime.Now;
        }
    }
}