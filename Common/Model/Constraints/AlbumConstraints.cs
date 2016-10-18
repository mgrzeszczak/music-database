using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping;

namespace Common.Model.Constraints
{
    public class AlbumConstraints : IAutoMappingOverride<Album>
    {
        public void Override(AutoMapping<Album> mapping)
        {
            mapping.Map(a => a.Name).UniqueKey("ARTIST_ALBUM"); //.Not.Nullable();
            mapping.References(a => a.Artist).UniqueKey("ARTIST_ALBUM");
        }
    }
}
