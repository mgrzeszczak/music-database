using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Validator.Constraints;

namespace Common.Model
{ 
    public class Song : Entity<long>
    {
        [NotNull]
        public virtual Album Album { get; set; }
        [NotNullNotEmpty]
        public virtual string Title { get; set; }
        public virtual TimeSpan Length { get; set; }
        public virtual int Number { get; set; }
        public virtual string YoutubeUrl { get; set; }
        public virtual string Lyrics { get; set; }

        public Song()
        {

        }

    }
}
