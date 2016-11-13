using Common.Communication;
using Common.Domain;
using Common.Model;
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
    class CreateArtistViewModel : ArtistViewModel
    {
        public CreateArtistViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider) : base(applicationViewModel,dataProvider)
        {
            Model = new Artist();
        }

        protected override void InitializeCommands()
        {
            Submit = new RelayCommand(o => {
                Response<Artist> response = DataProvider.SaveArtist(model);
                if (!response.Status)
                {
                    ApplicationViewModel.HandleError(response.Error);
                }
                else ApplicationViewModel.DisplayView.Execute(response.Content);
            });
            base.InitializeCommands();
        }

        public ICommand Submit { get; set; }

        

    }
}
