using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Common.Model.Constraints
{
    public class CommentConstraints : IAutoMappingOverride<Comment>
    {
        public void Override(AutoMapping<Comment> mapping)
        {
            mapping.References(c => c.User).Not.Nullable();
            //UniqueKey("COMMENT_USER_ENTITY")
            //mapping.Map(c => c.EntityId).UniqueKey("COMMENT_USER_ENTITY").Not.Nullable();
            //mapping.Map(c => c.EntityType).UniqueKey("COMMENT_USER_ENTITY").Not.Nullable();
            mapping.Map(c => c.Content).Length(500).Not.Nullable();
        }
    }
}
