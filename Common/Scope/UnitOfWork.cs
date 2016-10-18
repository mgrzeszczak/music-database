using NHibernate;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Scope
{
    
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISessionFactory sessionFactory;
        private readonly ITransaction transaction;

        public ISession Session { get; private set; }

        public UnitOfWork(ISessionFactory sessionFactory)
        {
            this.sessionFactory = sessionFactory;
            this.Session = sessionFactory.OpenSession();
            this.Session.FlushMode = FlushMode.Auto;
            this.transaction = Session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Dispose()
        {
            Session.Close();
        }

        public void Commit()
        {
            if (!transaction.IsActive)
            {
                throw new SystemException("Transaction is not active.");
            }
            transaction.Commit();
        }

        public void Rollback()
        {
            if (transaction.IsActive)
            {
                transaction.Rollback();
            }
        }
    }
}
