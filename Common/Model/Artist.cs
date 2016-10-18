
using Common.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Artist : Entity<long>
    {
        public virtual string Name { get; set; }
        public virtual IList<Album> Albums { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
