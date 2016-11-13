using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Loader.Criteria;

namespace Common.Model
{
    public abstract class Entity<ID>
    {
        public virtual ID Id {get; set;}
    }
}
