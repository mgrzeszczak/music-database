using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    public class Album
    {
        public virtual long Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Song> Songs { get; set; }
        public virtual int Number { get; set; }
        public virtual string CoverUrl { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
