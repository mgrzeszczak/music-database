using Common.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{ 
    public class Song
    {
        public virtual long Id { get; set; }
        public virtual string Title { get; set; }
        public virtual Album Album { get; set; }
        public virtual int Length { get; set; }
        public virtual int Number { get; set; }
    }
}
