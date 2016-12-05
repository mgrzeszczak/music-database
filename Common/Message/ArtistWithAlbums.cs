using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Model;

namespace Common.Message
{
    public class ArtistWithAlbums
    {
        public Artist Artist { get; }
        public IList<Album> Albums { get; }

        public ArtistWithAlbums(Artist artist, IList<Album> albums)
        {
            Artist = artist;
            Albums = albums;
        }
    }
}
