using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping;

namespace Common.Model.Constraints
{
    public class ArtistConstraints : IAutoMappingOverride<Artist>
    {
        public void Override(AutoMapping<Artist> mapping)
        {
            mapping.Map(a => a.Name).Unique();
        }
    }
}
