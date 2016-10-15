using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.NHibernate
{
    public class Context
    {
        private static ThreadLocal<IUnitOfWork> currentUnitOfWork = new ThreadLocal<IUnitOfWork>();

        public static void setUnitOfWork(IUnitOfWork unitOfWork)
        {
            currentUnitOfWork.Value = unitOfWork;
        }
        
        public static ISession provideSession()
        {
            return currentUnitOfWork.Value.Session;
        }

    }
}
