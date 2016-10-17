using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Communication
{
    public interface IDataProvider
    {
        IList<Song> searchSongs(string searchText);
        IList<Artist> searchArtists(string searchText);
        IList<Album> searchAlbums(string searchText);
        Artist saveArtist(Artist artist);
        Album saveAlbum(Album album);
    }
}
