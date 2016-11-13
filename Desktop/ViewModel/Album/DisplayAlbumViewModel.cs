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

namespace Desktop.ViewModel
{
    class DisplayAlbumViewModel : AlbumViewModel, ICacheable
    {

        public DisplayAlbumViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider, long albumId) : base(applicationViewModel,dataProvider)
        {
            Response<Album> respAlbum = DataProvider.GetAlbumById(albumId);
            Response<IList<Song>> respSongs = DataProvider.GetSongsFromAlbum(albumId);
            if (!respAlbum.Status || !respSongs.Status)
            {
                ApplicationViewModel.HandleError(!respAlbum.Status ? respAlbum.Error : respSongs.Error);
                return;
            }
            Songs = new ObservableCollection<Song>(respSongs.Content);
            Model = respAlbum.Content;
        }

        public ICommand Delete { get; set; }
        public ICommand CreateSong { get; set; }

        private ObservableCollection<Song> songs;
        public ObservableCollection<Song> Songs
        {
            get
            {
                return songs;
            }
            set
            {
                songs = value;
                OnPropertyChanged("Songs");
            }
        }

        public ViewModel GetViewModel()
        {
            return new DisplayAlbumViewModel(ApplicationViewModel, DataProvider, model.Id);
        }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            Delete = new RelayCommand(o =>
            {
                Response<bool> response = DataProvider.DeleteAlbum(model.Id);
                if (!response.Status) ApplicationViewModel.HandleError(response.Error);
                else
                {
                    ApplicationViewModel.DisplayView.Execute(model.Artist);
                    ApplicationViewModel.ClearHistory();
                }
            });
            CreateSong = new RelayCommand(o =>
            {
                ApplicationViewModel.CreateView.Execute(new CreateSongViewModel(ApplicationViewModel, DataProvider, model));
            });
        }
    }
}
