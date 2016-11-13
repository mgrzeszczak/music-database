using Common.Communication;
using Common.Domain;
using Common.Model;
using Desktop.Command;
using Desktop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Desktop.ViewModel
{
    class EditSongViewModel : SongViewModel
    {
        public EditSongViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider, long songId) : base(applicationViewModel,dataProvider)
        {
            Response<Song> response = DataProvider.GetSongById(songId);
            if (!response.Status)
            {
                ApplicationViewModel.HandleError(response.Error);
                return;
            }
            Model = response.Content;   
        }

        public ICommand Submit { get; set; }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            Submit = new RelayCommand(o => {
                Response<Song> response = DataProvider.UpdateSong(model);
                if (!response.Status)
                {
                    ApplicationViewModel.HandleError(response.Error);
                } else ApplicationViewModel.DisplayView.Execute(response.Content);
            });
        }

    }
}
