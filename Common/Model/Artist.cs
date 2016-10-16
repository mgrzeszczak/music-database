
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Artist
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Album> Albums { get; set; }
        public virtual string ImagePath { get; set; }
        public virtual IList<string> Genres { get; set; }
    }
}
