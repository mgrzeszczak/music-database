using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Common.Model;
using Desktop.Data;

namespace Desktop.ViewModel
{
    abstract class ViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModel()
        {
            InitializeCommands();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            System.Windows.Application.Current.Dispatcher.BeginInvoke(new Action(() => this.OnPropertyChanged(new PropertyChangedEventArgs(propertyName))));
        }

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            var handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void InitializeCommands()
        {

        }

        public User User => LoginSession.Authentication.User;
    }
}
