using Backend.Repository;
using Backend.Service;
using Common.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class ArtistServiceTests
    {

        private List<Song> songs;
        private List<Album> albums;
        private List<Artist> artists;

        [TestInitialize]
        public void Initialize()
        {
            Artist artist = new Artist() { Id = 1, Name = "Artist" };
            Album album = new Album() { Id = 1, Name = "Album" , Artist=artist};
            Song song = new Song() { Id = 1, Title = "Song" ,Album=album};

            songs = new List<Song>() { song };
            albums = new List<Album>() { album };
            artists = new List<Artist>() { artist };
        }

        [TestMethod]
        public void DeleteArtistTest()
        {
            Mock<ISongRepository> songRepositoryMock = new Mock<ISongRepository>();
            Mock<IAlbumRepository> albumRepositoryMock = new Mock<IAlbumRepository>();
            Mock<IArtistRepository> artistRepositoryMock = new Mock<IArtistRepository>();

            songRepositoryMock.Setup(s => s.Delete(It.IsAny<Song>())).Callback<Song>(s => songs.Remove(s));
            albumRepositoryMock.Setup(s => s.Delete(It.IsAny<Album>())).Callback<Album>(s => albums.Remove(s));
            artistRepositoryMock.Setup(s => s.Delete(It.IsAny<Artist>())).Callback<Artist>(s => artists.Remove(s));

            songRepositoryMock.Setup(s => s.FindOne(It.IsAny<long>())).Returns<long>(l => songs.Where(s => s.Id == l).SingleOrDefault());
            albumRepositoryMock.Setup(s => s.FindOne(It.IsAny<long>())).Returns<long>(l => albums.Where(s => s.Id == l).SingleOrDefault());
            artistRepositoryMock.Setup(s => s.FindOne(It.IsAny<long>())).Returns<long>(l => artists.Where(s => s.Id == l).SingleOrDefault());

            songRepositoryMock.Setup(s => s.FindAllByAlbumId(It.IsAny<long>())).Returns<long>(l => songs.Where(s => s.Album.Id == l).ToList());
            albumRepositoryMock.Setup(s => s.FindByArtistId(It.IsAny<long>())).Returns<long>(l => albums.Where(s => s.Artist.Id == l).ToList());

            ISongService songService = new SongService(songRepositoryMock.Object,albumRepositoryMock.Object);
            IAlbumService albumService = new AlbumService(albumRepositoryMock.Object,artistRepositoryMock.Object,songService);
            IArtistService artistService = new ArtistService(artistRepositoryMock.Object,albumService);

            artistService.Delete(1);

            Assert.IsTrue(artists.Count == 0 && albums.Count == 0 && songs.Count == 0);
        }

    }
}
