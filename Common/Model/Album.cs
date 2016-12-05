using System;
using System.Collections.Generic;
using NHibernate.Validator.Constraints;

namespace Common.Model
{
    public class Album : Entity<long>
    {
        [NotNullNotEmpty]
        public virtual string Name { get; set; }
        public virtual int Number { get; set; }
        public virtual string CoverUrl { get; set; }
        [NotNull]
        public virtual Artist Artist { get; set; }
        public virtual TimeSpan Length { get; set; }
        public virtual int Year { get; set; }

        public Album()
        {

        }
        
    }
}
