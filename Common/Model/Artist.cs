
using Common.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Validator.Constraints;

namespace Common.Model
{
    public class Artist : Entity<long>
    {
        [Length(Min = 1)]
        public virtual string Name { get; set; }
        public virtual string ImageUrl { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual string Description { get; set; }

        public Artist()
        {

        }
        
    }
}
