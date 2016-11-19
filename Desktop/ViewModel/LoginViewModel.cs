using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Common.Communication;
using Desktop.Command;

namespace Desktop.ViewModel
{
    class LoginViewModel : ChildViewModel
    {
        public LoginViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider) : base(applicationViewModel, dataProvider)
        {
            
        }

        protected string login;
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
        protected string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand LoginCommand { get; set; }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
            LoginCommand = new RelayCommand(a =>
            {
                
            });
        }
    }
}
