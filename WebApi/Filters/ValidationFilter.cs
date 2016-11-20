using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Common.Domain;
using Common.Exception;
using NHibernate.Validator.Engine;
using NHibernate.Validator.Event;
using Ninject.Web.Common;
using WebApi.App_Start;
using WebApi.Attributes;

namespace WebApi.Filters
{
    public class ValidationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var validatorEngine = NinjectHttpContainer.Resolve<NHibernateSharedEngineProvider>().GetEngine();

            var parameters = actionContext.ActionDescriptor.GetParameters();
            var arguments = actionContext.ActionArguments;

            List<InvalidValue> invalidValues = new List<InvalidValue>();
            Dictionary<string,object> validationErrors = new Dictionary<string, object>();

            foreach (var p in parameters)
            {
                var validate = p.GetCustomAttributes<Valid>().Count > 0;
                if (!validate) continue;
                var value = arguments[p.ParameterName];
                var errors = validatorEngine.Validate(value);
                invalidValues.AddRange(errors);
            }

            if (invalidValues.Count > 0)
            {
                foreach (var invalidValue in invalidValues)
                {
                    validationErrors.Add(invalidValue.PropertyName,invalidValue.Message);
                }
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, new ExceptionResponse("Validation exception",Error.VALIDATION_FAILED,validationErrors));
            }

        }
    }
}