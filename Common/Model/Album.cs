using System.Collections.Generic;

namespace Common.Model
{
    public class Album : Entity<long>
    {
        public virtual string Name { get; set; }
        public virtual IList<Song> Songs { get; set; }
        public virtual int Number { get; set; }
        public virtual string CoverUrl { get; set; }
        public virtual Artist Artist { get; set; }
    }
}
