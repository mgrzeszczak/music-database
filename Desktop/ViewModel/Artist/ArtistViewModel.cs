using Common.Communication;
using Common.Domain;
using Common.Model;
using Common.Model.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace Desktop.ViewModel
{
    class ArtistViewModel : ChildViewModel
    {
        protected Artist model;

        public ArtistViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider) : base(applicationViewModel,dataProvider)
        {
            model = new Artist();
        }

        public Artist Model
        {
            get { return model; }
            set
            {
                model = value;
                OnPropertyChanged(nameof(Model));
            }
        }

        public string Description
        {
            get { return model.Description; }
            set
            {
                model.Description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string Name
        {
            get { return model.Name; }
            set
            {
                model.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string ImageUrl
        {
            get { return model.ImageUrl; }
            set
            {
                model.ImageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }
        }

        public Genre Genre
        {
            get { return model.Genre; }
            set
            {
                model.Genre = value;
                OnPropertyChanged(nameof(Genre));
            }
        }

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
        }

    }
}
