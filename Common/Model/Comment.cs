using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model.Enum;
using NHibernate.Validator.Constraints;

namespace Common.Model
{
    public class Comment : Entity<long>
    {
        [NotNull]
        public virtual User User { get; set; }
        [NotNullNotEmpty]
        public virtual string Content { get; set; }
        public virtual long EntityId { get; set; }
        public virtual EntityType EntityType { get; set; }
        public virtual DateTime TimeStamp { get; set; }

    }
}
