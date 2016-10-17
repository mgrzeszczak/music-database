using Common.Model;
using Common.NHibernate;
using Desktop.Communication;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;

namespace TestDB
{
    class Program
    {
        static void Main(string[] args)
        {
            BackendDataProvider backendDataProvider = new BackendDataProvider();
            Artist artist1 = new Artist();
            artist1.Name = "Kings of Leon";

            Artist artist2 = new Artist();
            artist2.Name = "Coldplay";

            artist1 = backendDataProvider.saveArtist(artist1);
            artist2 = backendDataProvider.saveArtist(artist2);

            Album album1 = new Album();
            album1.Name = "Hello world!";
            album1.Artist = artist1;

            Album album2 = new Album();
            album2.Name = "Hello world!";
            album2.Artist = artist2;

            backendDataProvider.saveAlbum(album1);
            backendDataProvider.saveAlbum(album2);
        }
    }
}
