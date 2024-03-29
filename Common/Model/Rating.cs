﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model.Enum;
using NHibernate.Validator.Constraints;

namespace Common.Model
{
    public class Rating : Entity<long>
    {
        public virtual User User { get; set; }
        public virtual DateTime TimeStamp { get; set; }
        public virtual int Value { get; set; }
        public virtual EntityType EntityType { get; set; }
        public virtual long EntityId { get; set; }
    }
}
