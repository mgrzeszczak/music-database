using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model.Enum;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Common.Model.Constraints
{
    public class UserConstraints : IAutoMappingOverride<User>
    {
        public void Override(AutoMapping<User> mapping)
        {
            mapping.Map(a => a.Login).Unique().Not.Nullable();
            mapping.Map(a => a.Role).Not.Nullable();
            mapping.Map(a => a.Password).Not.Nullable();
        }
    }
}
