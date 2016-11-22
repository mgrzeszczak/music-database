using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.Communication;
using Common.Model;
using Common.Model.Form;
using Desktop.Command;
using Desktop.Data;

namespace Desktop.ViewModel
{
    class RegisterViewModel : ChildViewModel
    {
        public RegisterViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider) : base(applicationViewModel, dataProvider)
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
       
        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public ICommand RegisterCommand { get; set; }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            RegisterCommand = new RelayCommand(a =>
            {
                string pass = a as string;
                RegisterForm form = new RegisterForm
                {
                    Login = login,
                    Password = pass,
                    Email = email
                };
                RestClient.ExecuteAsync<User>(RequestFactory.RegisterRequest(form), (resp, handle) =>
                {
                    if (resp.Succeeded()) ApplicationViewModel.LoginPage.Execute(null);
                    else ApplicationViewModel.HandlExceptionResponse(resp.ExceptionResponse());
                });
            });
        }

    }
}
