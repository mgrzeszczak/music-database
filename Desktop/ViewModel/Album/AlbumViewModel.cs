using Common.Communication;
using Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModel
{
    class AlbumViewModel : ChildViewModel
    {
        protected Album model;

        public AlbumViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider) : base(applicationViewModel,dataProvider)
        {
            model = new Album();
        }

        public Album Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public TimeSpan Length
        {
            get { return model.Length; }
            set
            {
                model.Length = value;
                OnPropertyChanged("Length");
            }
        }

        public string NameYear
        {
            get { return string.Format("{0} ({1})", model.Name, model.Year); }
            set
            {

            }
        }

        public int Year
        {
            get { return model.Year; }
            set
            {
                model.Year = value;
                OnPropertyChanged("Year");
            }
        }

        public string Name
        {
            get { return model.Name; }
            set
            {
                model.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public int Number
        {
            get { return model.Number; }
            set
            {
                model.Number = value;
                OnPropertyChanged("Number");
            }
        }
        public string CoverUrl
        {
            get { return model.CoverUrl; }
            set
            {
                model.CoverUrl = value;
                OnPropertyChanged("CoverUrl");
            }
        }
        public Artist Artist
        {
            get { return model.Artist; }
            set
            {
                model.Artist = value;
                OnPropertyChanged("Artist");
            }
        }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
        }
    }
}
