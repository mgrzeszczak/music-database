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
using Backend.Scope;
using Common.Model.Constraints;
using Common.Communication;
using Backend.Config;
using System.Collections.Generic;
using Backend.Repository;
using Common.Domain;
using System.Collections;

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
                .Mappings(m => {
                        var map = AutoMap.AssemblyOf<Song>(storeCfg);
                        map.UseOverridesFromAssemblyOf<SongConstraints>();
                        map.Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never());
                        m.AutoMappings.Add(map);
                    }
                )
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
                    throw new BusinessException(e.InnerException!=null ? RecognizeError(e.InnerException,e.Message):Error.UNKNOWN_ERROR);
                }
            }   
        }

        private Error RecognizeError(System.Exception e, string message)
        {
            string innerMessage = e.Message;
            if (innerMessage.Contains("Duplicate"))
            {
                if (innerMessage.Contains("Name"))
                {
                    if (message.Contains("Model.Album"))
                    {
                        return Error.ALBUM_NAME_TAKEN;
                    }
                    if (message.Contains("Model.Artist"))
                    {
                        return Error.ARTIST_NAME_TAKEN;
                    }
                }
                if (innerMessage.Contains("Number"))
                {
                    if (message.Contains("Model.Album"))
                    {
                        return Error.ALBUM_NUMBER_TAKEN;
                    }
                    if (message.Contains("Model.Song"))
                    {
                        return Error.SONG_NUMBER_TAKEN;
                    }
                }
                if (innerMessage.Contains("Title"))
                {
                    return Error.SONG_TITLE_TAKEN;
                }
            }

            return Error.UNKNOWN_ERROR;
        }

        public class NumberComparer : IComparer<int>
        {
            public int Compare(int x, int y)
            {
                return x - y;
            }
        }


        public Page<Song> SearchSongsByTitle(string searchText, int pageNr, int amountPerPage)
        {
            return this.WrapInUnitOfWork(() =>this.songService.SearchBy(searchText,s=>s.Number,new NumberComparer(),pageNr,amountPerPage));
        }

        public Page<Artist> SearchArtistsByName(string searchText, int pageNr, int amountPerPage)
        {
            return this.WrapInUnitOfWork(() => this.artistService.SearchBy(searchText, pageNr, amountPerPage));
        }

        public Page<Album> SearchAlbumsByName(string searchText, int pageNr, int amountPerPage)
        {
            return this.WrapInUnitOfWork(() => this.albumService.SearchBy(searchText, s => s.Year, new NumberComparer(), pageNr, amountPerPage));
        }

        public Song SaveSong(Song song)
        {
            return this.WrapInUnitOfWork(() => songService.Add(song));
        }

        public Artist SaveArtist(Artist artist)
        {
            return this.WrapInUnitOfWork(() => artistService.Save(artist));
        }

        public Album SaveAlbum(Album album)
        {
            return this.WrapInUnitOfWork(() => albumService.Add(album));
        }

        public Song UpdateSong(Song song)
        {
            return this.WrapInUnitOfWork(() => songService.Update(song));
        }

        public Album UpdateAlbum(Album album)
        {
            return this.WrapInUnitOfWork(() => albumService.Update(album));
        }

        public Artist UpdateArtist(Artist artist)
        {
            return this.WrapInUnitOfWork(() => artistService.Update(artist));
        }

        public void DeleteSong(long id)
        {
            this.WrapInUnitOfWork<Object>(() => { songService.Delete(id); return null; });
        }

        public void DeleteAlbum(long id)
        {
            this.WrapInUnitOfWork<Object>(() => { albumService.Delete(id); return null; });
        }

        public void DeleteArtist(long id)
        {
            this.WrapInUnitOfWork<Object>(() => { artistService.Delete(id); return null; });
        }

        public Song GetSongById(long id)
        {
            return this.WrapInUnitOfWork(() => songService.FindById(id));
        }

        public Album GetAlbumById(long id)
        {
            return this.WrapInUnitOfWork(() => albumService.FindById(id));
        }

        public Artist GetArtistById(long id)
        {
            return this.WrapInUnitOfWork(() => artistService.FindById(id));
        }

        
        public IList<Song> GetSongsFromAlbum(long albumId)
        {
            return this.WrapInUnitOfWork(() => songService.FindByAlbumId(albumId));
        }

        public IList<Album> GetAlbumsByArtist(long artistId)
        {
            return this.WrapInUnitOfWork(() => albumService.FindByArtistId(artistId));
        }

      
    }
}
