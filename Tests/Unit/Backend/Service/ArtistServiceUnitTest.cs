using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Backend.Repository;
using Backend.Service;
using Common.Model;
using Moq;

namespace Tests
{
    [TestClass]
    public class ArtistServiceUnitTest
    {
        private IArtistRepository artistRepository;
        private IArtistService artistService;

        public ArtistServiceUnitTest()
        {
            var moq = new Mock<IArtistRepository>();
            Artist artist = new Artist();
            artist.Id = 1;
            artist.Name = "test";
            moq.Setup(r => r.FindOne(1)).Returns(artist);
            artistRepository = moq.Object;
            artistService = new ArtistService(artistRepository);
        }

        [TestMethod]
        public void test()
        {
            Assert.IsTrue(artistService.FindById(1).Id == 1);
        }

    }
}
