using NHibernate;
using System.Threading;

namespace Common.Scope
{
    public class Context
    {
        private static ThreadLocal<IUnitOfWork> currentUnitOfWork = new ThreadLocal<IUnitOfWork>();

        public static void SetUnitOfWork(IUnitOfWork unitOfWork)
        {
            currentUnitOfWork.Value = unitOfWork;
        }
        
        public static ISession ProvideSession()
        {
            return currentUnitOfWork.Value.Session;
        }

    }
}
