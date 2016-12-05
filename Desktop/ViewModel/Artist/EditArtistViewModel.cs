using Common.Communication;
using Common.Domain;
using Common.Model;
using Desktop.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Desktop.Data;

namespace Desktop.ViewModel
{
    class EditArtistViewModel : ArtistViewModel
    {
        public EditArtistViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider, long artistId) : base(applicationViewModel, dataProvider)
        {
            ApplicationViewModel.RestClient.ExecuteAsync<Artist>(ApplicationViewModel.RequestFactory.GetArtistRequest(artistId),
                    (r, c) =>
                    {                        
                        if (r.Succeeded()) Model = r.Data;
                        else ApplicationViewModel.HandlExceptionResponse(r.ExceptionResponse());
                    });
            /*Response<Artist> respArtist = DataProvider.GetArtistById(artistId);
            if (!respArtist.Status)
            {
                ApplicationViewModel.HandleError(respArtist.Error);
                return;
            }
            Model = respArtist.Content;*/
        }

        protected override void InitializeCommands()
        {
            Submit = new RelayCommand(o => {
                ApplicationViewModel.RestClient.ExecuteAsync<Artist>(ApplicationViewModel.RequestFactory.UpdateArtistRequest(model),
                    (r, c) =>
                    {
                        if (r.Succeeded()) ApplicationViewModel.DisplayView.Execute(r.Data);
                        else ApplicationViewModel.HandlExceptionResponse(r.ExceptionResponse());
                    });
                /*Response<Artist> response = DataProvider.UpdateArtist(model);
                if (!response.Status)
                {
                    ApplicationViewModel.HandleError(response.Error);
                }
                else ApplicationViewModel.DisplayView.Execute(response.Content);*/
            });
            base.InitializeCommands();
        }

        public ICommand Submit { get; set; }
    }
}
