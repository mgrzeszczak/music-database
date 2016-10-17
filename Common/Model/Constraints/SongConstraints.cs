using FluentNHibernate.Automapping.Alterations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Automapping;

namespace Common.Model.Constraints
{
    public class SongConstraints : IAutoMappingOverride<Song>
    {
        public void Override(AutoMapping<Song> mapping)
        {
            mapping.Map(s => s.Title).UniqueKey("ALBUM_SONG");
            mapping.References(s => s.Album).UniqueKey("ALBUM_SONG");
            //mapping.Map(s => s.Album).UniqueKey("ALBUM_SONG");
        }
    }
}
