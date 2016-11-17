using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Backend.Scope;
using NHibernate;
using WebApi.Attributes;

namespace WebApi.App_Start
{
    public class TransactionFilter : ActionFilterAttribute
    {
        private readonly ISessionFactory sessionFactory;
        private readonly IDictionary<int, IUnitOfWork> requestToUnitOfWorkDictionary = new Dictionary<int, IUnitOfWork>();

        public TransactionFilter(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var unitOfWork = new UnitOfWork(sessionFactory);
            int hashCode = actionContext.GetHashCode();
            requestToUnitOfWorkDictionary.Add(new KeyValuePair<int, IUnitOfWork>(hashCode,unitOfWork));
            Context.SetUnitOfWork(unitOfWork);
            //var filter in actionContext.ActionDescriptor.GetCustomAttributes<Transactional>(false))
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            int hashCode = actionExecutedContext.ActionContext.GetHashCode();
            if (!requestToUnitOfWorkDictionary.ContainsKey(hashCode)) return;
            IUnitOfWork unitOfWork = requestToUnitOfWorkDictionary[hashCode];
            if (actionExecutedContext.Exception != null)
            {
                unitOfWork.Rollback();
            }
            else
            {
                unitOfWork.Commit();
                unitOfWork.Dispose();
            }
            requestToUnitOfWorkDictionary.Remove(hashCode);
        }

    }
}