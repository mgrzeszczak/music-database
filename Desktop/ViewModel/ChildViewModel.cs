using Common.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        protected override void InitializeCommands()
        {
            base.InitializeCommands();
        }

    }
}
