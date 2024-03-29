﻿using FluentNHibernate.Automapping.Alterations;
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
            mapping.Map(s => s.Title).UniqueKey("ALBUM_SONG").Not.Nullable();
            mapping.References(s => s.Album).UniqueKey("ALBUM_SONG").Not.Nullable();

            mapping.Map(s => s.Number).UniqueKey("ALBUM_SONG_NUMBER");
            mapping.References(s => s.Album).UniqueKey("ALBUM_SONG_NUMBER");

            mapping.Map(s => s.Lyrics).Length(20000);
        }
    }
}
