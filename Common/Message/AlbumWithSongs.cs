using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Common.Message
{
    public class AlbumWithSongs
    {
        public Album Album { get; }
        public IList<Song> Songs { get; }

        public AlbumWithSongs(Album album, IList<Song> songs)
        {
            Album = album;
            Songs = songs;
        }
    }
}
