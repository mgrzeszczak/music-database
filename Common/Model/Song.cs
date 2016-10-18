using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{ 
    public class Song : Entity<long>
    {
        public virtual string Title { get; set; }
        public virtual Album Album { get; set; }
        public virtual int Length { get; set; }
        public virtual int Number { get; set; }
        public virtual string YoutubeUrl { get; set; }
        public virtual string Lyrics { get; set; }
    }
}
