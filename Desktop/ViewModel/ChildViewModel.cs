using Common.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Desktop.Command;
using Desktop.Data;
using RestSharp;

namespace Desktop.ViewModel
{
    class ChildViewModel : ViewModel
    {
        public ChildViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider)
        {
            ApplicationViewModel = applicationViewModel;
            DataProvider = dataProvider;
            SearchText = "";
        }

        protected string searchText;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }


        public ApplicationViewModel ApplicationViewModel { get; set; }
        public IResponseDataProvider DataProvider { get; set; }
        public IRestClient RestClient => ApplicationViewModel.RestClient;
        public IRestRequestFactory RequestFactory => ApplicationViewModel.RequestFactory;

        public ICommand Logout { get; set; }
        protected override void InitializeCommands()
        {
            Logout = new RelayCommand(o =>
            {
                RestClient.ExecuteAsync(RequestFactory.LogoutRequest(), (resp, handle) =>
                {
                    if (resp.Succeeded())
                    {
                        ApplicationViewModel.LoginPage.Execute(null);
                    } else ApplicationViewModel.HandlExceptionResponse(resp.ExceptionResponse());
                });
            });
            base.InitializeCommands();
        }

    }
}
