﻿using Common.Exception;
using Common.NHibernate;
using NHibernate;
using Ninject;
using Backend.Service;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Automapping;
using Common.Model;
using System;
using System.Collections.Generic;
using Common.Scope;
using Common.Model.Constraints;

namespace Desktop.Communication
{
    public class BackendDataProvider : IDataProvider
    {
        private ISessionFactory sessionFactory;
        private SongService songService;
        private ArtistService artistService;
        private AlbumService albumService;
        private IKernel kernel;

        public BackendDataProvider()
        {
            initDatabaseConnection();
            this.kernel = new StandardKernel();
            songService = kernel.Get<SongService>();
            artistService = kernel.Get<ArtistService>();
            albumService = kernel.Get<AlbumService>();
        }

        private void initDatabaseConnection()
        {
            var storeCfg = new StoreConfiguration();
            var fluentConfig = Fluently.Configure().Database(
             MySQLConfiguration.Standard.ConnectionString(
                 c => c.Is("Server=localhost;Database=lyrics;User=root;Password=root;")))
             .Mappings(m => 
                m.AutoMappings.Add(AutoMap.AssemblyOf<Song>(storeCfg).UseOverridesFromAssemblyOf<SongConstraints>()))
             .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true));
            var conf = fluentConfig.BuildConfiguration();
            this.sessionFactory = conf.BuildSessionFactory();
        }

        private T wrapInUnitOfWork<T>(System.Func<T> f)
        {
            IUnitOfWork unitOfWork = new UnitOfWork(sessionFactory);
            Context.setUnitOfWork(unitOfWork);
            try
            {
                T ret = f();
                unitOfWork.Commit();
                return ret;
            }
            catch (Exception e)
            {
                unitOfWork.Rollback();
                throw new BusinessException(e.Message);
            }
        }

        public IList<Song> searchSongs(string searchText)
        {
            return wrapInUnitOfWork(()=>songService.searchSongs(searchText));
        }

        public IList<Artist> searchArtists(string searchText)
        {
            return wrapInUnitOfWork(() => artistService.searchArtists(searchText));
        }

        public IList<Album> searchAlbums(string searchText)
        {
            return wrapInUnitOfWork(() => albumService.searchAlbums(searchText));
        }

        public Album saveAlbum(Album album)
        {
            return wrapInUnitOfWork(() => albumService.save(album));
        }

        public Artist saveArtist(Artist artist)
        {
            return wrapInUnitOfWork(() => artistService.save(artist));
        }
        
    }
}
