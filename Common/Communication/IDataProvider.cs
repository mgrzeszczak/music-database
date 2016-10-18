using Common.Model;
using System.Collections.Generic;

namespace Common.Communication
{
    public interface IDataProvider
    {
        IList<Song> SearchSongsByTitle(string searchText);
        IList<Artist> SearchArtistsByName(string searchText);
        IList<Album> SearchAlbumsByName(string searchText);

        Song SaveSong(Song song);
        Artist SaveArtist(Artist artist);
        Album SaveAlbum(Album album);
    }
}
