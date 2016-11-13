using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.ViewModel;

namespace Desktop.Cache
{
    interface IBrowsingHistory
    {
        Desktop.ViewModel.ViewModel GetPreviousPage();
        void Add(Desktop.ViewModel.ViewModel page);
        void Clear();        
    }
}
