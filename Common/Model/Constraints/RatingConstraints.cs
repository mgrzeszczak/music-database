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
    public class RatingConstraints : IAutoMappingOverride<Rating>
    {
        public void Override(AutoMapping<Rating> mapping)
        {
            mapping.References(r => r.User).UniqueKey("RATING_USER_ENTITY").Not.Nullable();
            mapping.Map(r => r.EntityType).UniqueKey("RATING_USER_ENTITY").Not.Nullable();
            mapping.Map(r => r.EntityId).UniqueKey("RATING_USER_ENTITY").Not.Nullable();
        }
    }
}
