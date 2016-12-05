using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.Authorization;
using Common.Communication;
using Desktop.Command;
using Desktop.Data;

namespace Desktop.ViewModel
{
    class LoginViewModel : ChildViewModel
    {
        public LoginViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider) : base(applicationViewModel, dataProvider)
        {
            
        }

        private string login;
        public string Login
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public ICommand LoginCommand { get; set; }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            LoginCommand = new RelayCommand(a =>
            {
                string pass = a as string;
                RestClient.ExecuteAsync<Authentication>(RequestFactory.LoginRequest(login, pass), (resp, handle) =>
                {
                    Console.WriteLine(resp.Content);
                    if (resp.Succeeded())
                    {
                        LoginSession.Authentication = resp.Data;
                        ApplicationViewModel.Home.Execute(null);
                    } else ApplicationViewModel.HandlExceptionResponse(resp.ExceptionResponse());
                });
            });
        }
    }
}
