﻿using Common.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModel
{
    class HomeViewModel : ChildViewModel
    {
        public HomeViewModel(ApplicationViewModel applicationViewModel, IResponseDataProvider dataProvider) : base(applicationViewModel, dataProvider)
        {

        }
    }
}
