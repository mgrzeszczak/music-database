 using Common.Communication;
using Common.Domain;
using Common.Model;
using Desktop.Cache;
using Desktop.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.ViewModel
{
    class DisplaySongViewModel : SongViewModel, ICacheable
    {

        public DisplaySongViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider, long songId) : base(applicationViewModel,dataProvider)
        {
            /*Response<Song> response = DataProvider.GetSongById(songId);
            if (!response.Status)
            {
                ApplicationViewModel.HandleError(response.Error);
                return;
            }
            Model = response.Content;*/
            ApplicationViewModel.RestClient.ExecuteAsync<Song>(
                ApplicationViewModel.RequestFactory.GetSongRequest(songId),
                (resp,handle) =>
                {
                    if (resp.Data!=null) Model = resp.Data;
                });
        }

        private bool isClosing;
        public bool IsClosing
        {
            get
            {
                return isClosing;
            }
            set
            {
                isClosing = value;
                OnPropertyChanged(nameof(IsClosing));
            }
        }

        public ICommand Home { get; set; }
        public ICommand Back { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Delete { get; set; }

        public ViewModel GetViewModel()
        {
            return new DisplaySongViewModel(ApplicationViewModel, DataProvider, model.Id);
        }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            Delete = new RelayCommand(o =>
            {
                IsClosing = true;
                Response<bool> response = DataProvider.DeleteSong(model.Id);
                if (!response.Status) ApplicationViewModel.HandleError(response.Error);
                else
                {
                    ApplicationViewModel.DisplayView.Execute(model.Album);
                    ApplicationViewModel.ClearHistory();
                }
            });
            Home = new RelayCommand(o =>
            {
                IsClosing = true;
                ApplicationViewModel.Home.Execute(null);
            });
            Back = new RelayCommand(o =>
            {
                IsClosing = true;
                ApplicationViewModel.PreviousPage.Execute(null);
            });
            Edit = new RelayCommand(o =>
            {
                IsClosing = true;
                ApplicationViewModel.EditView.Execute(Model);
            });
        }

    }
}
