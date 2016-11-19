using Common.Communication;
using Common.Domain;
using Common.Model;
using Desktop.Cache;
using Desktop.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.Message;
using Desktop.Data;

namespace Desktop.ViewModel
{
    class DisplayArtistViewModel : ArtistViewModel, ICacheable
    {

        public DisplayArtistViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider, long artistId) : base(applicationViewModel, dataProvider)
        {
            RestClient.ExecuteAsync<ArtistWithAlbums>(RequestFactory.GetArtistWithAlbumsRequest(artistId),
                    (r, c) =>
                    {
                        if (r.Succeeded())
                        {
                            Albums = new ObservableCollection<Album>(r.Data.Albums);
                            Model = r.Data.Artist;

                            Console.WriteLine(Model.Name);

                        }
                        else ApplicationViewModel.HandlExceptionResponse(r.ExceptionResponse());
                    });
            /*Response<Artist> respArtist = DataProvider.GetArtistById(artistId);
            Response<IList<Album>> respAlbums = DataProvider.GetAlbumsByArtist(artistId);
            if (!respArtist.Status || !respAlbums.Status)
            {
                ApplicationViewModel.HandleError(!respArtist.Status ? respArtist.Error : respAlbums.Error);
                return;
            }
            Albums = new ObservableCollection<Album>(respAlbums.Content);
            Model = respArtist.Content;*/
        }

        public ICommand Delete { get; set; }
        public ICommand CreateAlbum { get; set; }

        private ObservableCollection<Album> albums;
        public ObservableCollection<Album> Albums
        {
            get
            {
                return albums;
            }
            set
            {
                albums = value;
                OnPropertyChanged(nameof(Albums));
            }
        }

        public ViewModel GetViewModel()
        {
            return new DisplayArtistViewModel(ApplicationViewModel, DataProvider, model.Id);
        }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            Delete = new RelayCommand(o =>
            {
                ApplicationViewModel.RestClient.ExecuteAsync<Artist>(ApplicationViewModel.RequestFactory.DeleteArtistRequest(model.Id),
                    (r, c) =>
                    {
                        if (r.Succeeded())
                        {
                            ApplicationViewModel.Search.Execute(model.Name);
                            ApplicationViewModel.ClearHistory();
                        }
                        else ApplicationViewModel.HandlExceptionResponse(r.ExceptionResponse());
                    });
                /*
                Response<bool> response = DataProvider.DeleteArtist(model.Id);
                if (!response.Status) ApplicationViewModel.HandleError(response.Error);
                else
                {
                    ApplicationViewModel.Search.Execute(model.Name);
                    ApplicationViewModel.ClearHistory();
                }*/
            });
            CreateAlbum = new RelayCommand(o =>
            {
                ApplicationViewModel.CreateView.Execute(new CreateAlbumViewModel(ApplicationViewModel, DataProvider, model));
            });
        }
    }
}
