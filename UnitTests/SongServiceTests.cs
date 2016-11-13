using Backend.Repository;
using Backend.Service;
using Common.Exception;
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
    public class SongServiceTests
    {

        private List<Song> songs;

        [TestInitialize]
        public void Initialize()
        {
            Song song1 = new Song();
            song1.Title = "Received song1";
            song1.Id = 1;
            Song song2 = new Song();
            song2.Title = "Received song2";
            song2.Id = 2;
            songs = new List<Song>() { song1, song2 };
        }

        [TestMethod]
        public void FindSongsByAlbumIdTest()
        {
            Mock<ISongRepository> songRepository = new Mock<ISongRepository>();
            Song song1 = new Song();
            song1.Title = "Received song1";
            song1.Id = 1;
            Song song2 = new Song();
            song2.Title = "Received song2";
            song2.Id = 2;
            songRepository.Setup(r => r.FindAllByAlbumId(1)).Returns(new List<Song>() { song1,song2 });
            ISongService songService = new SongService(songRepository.Object,null);

            Assert.AreEqual(songService.FindByAlbumId(1)[0], song1);
            Assert.AreEqual(songService.FindByAlbumId(1)[1], song2);
            Assert.IsTrue(songService.FindByAlbumId(1).Count == 2);
        }

        [TestMethod]
        public void AddSongTest()
        {
            Mock<ISongRepository> songRepositoryMock = new Mock<ISongRepository>();
            Mock<IAlbumRepository> albumRepositoryMock = new Mock<IAlbumRepository>();

            songRepositoryMock.Setup(s => s.Save(It.IsAny<Song>())).Callback<Song>(s => songs.Add(s)).Returns<Song>(s=>s);
            albumRepositoryMock.Setup(a => a.FindOne(1)).Returns(new Album { Id = 1, Name = "TestAlbum" });

            ISongService songService = new SongService(songRepositoryMock.Object, albumRepositoryMock.Object);

            Song song = new Song { Id = 1, Album = new Album { Id = 1 }, Title = "TestSong" };

            song = songService.Add(song);
            Assert.AreEqual(song.Album.Name, "TestAlbum");
        }

        [TestMethod]
        public void DeleteSongTest()
        {
            Mock<ISongRepository> songRepositoryMock = new Mock<ISongRepository>();

            var songId1 = songs.Where(s => s.Id == 1).ToList();

            songRepositoryMock.Setup(s => s.Delete(It.IsAny<Song>())).Callback<Song>(s => songs.Remove(s));
            songRepositoryMock.Setup(s => s.FindOne(It.IsAny<long>())).Returns<long>(l=>songs.Where(s => s.Id == l).SingleOrDefault());
            ISongService songService = new SongService(songRepositoryMock.Object, null);

            songService.Delete(1);
            Assert.AreEqual(null,songService.FindById(1));
            songService.Delete(1);
        }

        [TestMethod]
        public void UpdateSongTest()
        {
            Mock<ISongRepository> songRepositoryMock = new Mock<ISongRepository>();
            Mock<IAlbumRepository> albumRepositoryMock = new Mock<IAlbumRepository>();            

            songRepositoryMock.Setup(s => s.Update(It.IsAny<Song>())).Callback<Song>(s =>
            {
                songs = songs.Where(s2 => s2.Id != s.Id).ToList();
                songs.Add(s);
            });
            songRepositoryMock.Setup(s => s.FindOne(1)).Returns(songs.Where(s => s.Id == 1).SingleOrDefault());

            albumRepositoryMock.Setup(a => a.FindOne(1)).Returns(new Album { Id = 1, Name = "TestAlbum" });

            ISongService songService = new SongService(songRepositoryMock.Object, albumRepositoryMock.Object);

            Song song = songService.FindById(1);
            song.Album = new Album { Id = 1 };
            song.Title = "ChangedTitle";
            songService.Update(song);

            song = songService.FindById(1);
            Assert.AreEqual(song.Title, "ChangedTitle");
        }



    }

}
