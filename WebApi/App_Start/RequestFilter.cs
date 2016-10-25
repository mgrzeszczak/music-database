using Common.Scope;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace WebApi.App_Start
{
    public class RequestFilter : ActionFilterAttribute
    {

        private IUnitOfWork unitOfWork;
        private ISessionFactory sessionFactory;

        public RequestFilter(ISessionFactory sessionFactory){
            this.sessionFactory = sessionFactory;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            unitOfWork = new UnitOfWork(sessionFactory);

            var routeData = actionContext.ControllerContext.RouteData;

            //If you don't have an action name, I've assumed "index" is the default.
            var actionName = routeData.Values.ContainsKey("id") ? routeData.Values["id"].ToString() : "Index";



            //you can then get the method via reflection...
            /*var attribs = actionContext.ControllerContext.Controller.GetType()
                        .GetMethod(actionName, BindingFlags.Public | BindingFlags.Instance)
                        .GetCustomAttributes();*/

            Context.SetUnitOfWork(unitOfWork);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception!= null)
            {
                unitOfWork.Rollback();
            } else
            {
                unitOfWork.Commit();
                unitOfWork.Dispose();
            }
        }

    }
}