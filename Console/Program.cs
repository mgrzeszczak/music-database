using Backend.Communication;
using Common.Communication;
using Common.Model;

namespace Console
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataProvider backendDataProvider = new BackendDataProvider();

            /*
            Artist artist = new Artist();
            artist.Name = "Kings of Leon";
            artist.Genre = Common.Model.Enum.Genre.ROCK;
            artist.ImageUrl = "someurl"; */

            
            Artist artist = backendDataProvider.SearchArtistsByName("kings")[0];
            artist.Name = "Hello world";
            backendDataProvider.UpdateArtist(artist);
            /*
            Album album = new Album();
            album.Name = "Youth and Young Manhood";
            album.Number = 1;
            album.CoverUrl = "someurl";

            Song song = new Song();
            song.Title = "Happy Alone";
            song.Length = TimeSpan.FromSeconds(4.5);
            song.Lyrics = "passing time";
            song.YoutubeUrl = "someurl";
            song.Number = 1;

            album.Artist = artist;
            song.Album = album;
            
            backendDataProvider.SaveArtist(artist);
            backendDataProvider.SaveAlbum(album);
            backendDataProvider.SaveSong(song);*/
        }
    }
}
