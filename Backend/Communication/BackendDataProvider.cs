using Common.Exception;
using NHibernate;
using Ninject;
using Backend.Service;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using FluentNHibernate.Automapping;
using Common.Model;
using System;
using Common.Scope;
using Common.Model.Constraints;
using Common.Communication;
using Backend.Config;
using System.Collections.Generic;
using Backend.Repository;

namespace Backend.Communication
{
    public class BackendDataProvider : IDataProvider
    {
        private ISessionFactory sessionFactory;

        private ISongService songService;
        private IArtistService artistService;
        private IAlbumService albumService;

        private IKernel container;

        public BackendDataProvider()
        {
            this.InitDatabaseConnection();
            this.InitDependencyInjectionContainer();
        }

        private void InitDependencyInjectionContainer()
        {
            this.container = new StandardKernel();

            this.container.Bind<ISongRepository>().To<SongRepository>();
            this.container.Bind<IArtistRepository>().To<ArtistRepository>();
            this.container.Bind<IAlbumRepository>().To<AlbumRepository>();

            this.container.Bind<IAlbumService>().To<AlbumService>();
            this.container.Bind<IArtistService>().To<ArtistService>();
            this.container.Bind<ISongService>().To<SongService>();

            this.songService = this.container.Get<ISongService>();
            this.artistService = this.container.Get<IArtistService>();
            this.albumService = this.container.Get<IAlbumService>();
        }

        private void InitDatabaseConnection()
        {
            var storeCfg = new ModelConfig();
            var fluentConfig = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(c => c.Is(DatabaseConfig.ConnectionString)))
                .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Song>(storeCfg).UseOverridesFromAssemblyOf<SongConstraints>()))
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true));
            var conf = fluentConfig.BuildConfiguration();
            this.sessionFactory = conf.BuildSessionFactory();
        }

        private T WrapInUnitOfWork<T>(System.Func<T> f)
        {
            using (IUnitOfWork unitOfWork = new UnitOfWork(sessionFactory))
            {
                Context.SetUnitOfWork(unitOfWork);
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
        }

        public IList<Song> SearchSongsByTitle(string searchText, int pageNr, int amountPerPage)
        {
            return this.WrapInUnitOfWork(() =>this.songService.SearchBy(searchText,pageNr,amountPerPage));
        }

        public IList<Artist> SearchArtistsByName(string searchText, int pageNr, int amountPerPage)
        {
            return this.WrapInUnitOfWork(() => this.artistService.SearchBy(searchText, pageNr, amountPerPage));
        }

        public IList<Album> SearchAlbumsByName(string searchText, int pageNr, int amountPerPage)
        {
            return this.WrapInUnitOfWork(() => this.albumService.SearchBy(searchText, pageNr, amountPerPage));
        }

        public Song SaveSong(Song song)
        {
            return this.WrapInUnitOfWork(() => songService.Save(song));
        }

        public Artist SaveArtist(Artist artist)
        {
            return this.WrapInUnitOfWork(() => artistService.Save(artist));
        }

        public Album SaveAlbum(Album album)
        {
            return this.WrapInUnitOfWork(() => albumService.Save(album));
        }

        public void UpdateSong(Song song)
        {
            this.WrapInUnitOfWork<Object>(() => { songService.Update(song); return null; });
        }

        public void UpdateAlbum(Album album)
        {
            this.WrapInUnitOfWork<Object>(() => { albumService.Update(album); return null; });
        }

        public void UpdateArtist(Artist artist)
        {
            this.WrapInUnitOfWork<Object>(() => { artistService.Update(artist); return null; });
        }

        public void DeleteSong(Song song)
        {
            this.WrapInUnitOfWork<Object>(() => { songService.Delete(song); return null; });
        }

        public void DeleteAlbum(Album album)
        {
            this.WrapInUnitOfWork<Object>(() => { albumService.Delete(album); return null; });
        }

        public void DeleteArtist(Artist artist)
        {
            this.WrapInUnitOfWork<Object>(() => { artistService.Delete(artist); return null; });
        }
    }
}
