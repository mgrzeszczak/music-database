using Backend.Communication;
using Common.Communication;
using Common.Model;
using System;
using System.Collections.Generic;

namespace BackendTester
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataProvider backendDataProvider = new BackendDataProvider();

            /*Artist artist = new Artist();
            artist.Name = "Kings of Leon";
            artist.Genre = Common.Model.Enum.Genre.ROCK;
            artist.ImageUrl = "someurl";
            
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
