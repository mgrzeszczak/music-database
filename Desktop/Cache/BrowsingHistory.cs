using Desktop.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Cache
{
    class BrowsingHistory : IBrowsingHistory
    {
        private Stack<ICacheable> stack;
        private ApplicationViewModel applicationViewModel;

        public BrowsingHistory(ApplicationViewModel applicationViewModel)
        {
            this.stack = new Stack<ICacheable>();
            this.applicationViewModel = applicationViewModel;
        }

        public Desktop.ViewModel.ViewModel GetPreviousPage()
        {
            return stack.Count==0? applicationViewModel.DefaultPage() : stack.Pop().GetViewModel();
        }

        public void Add(Desktop.ViewModel.ViewModel page)
        {
            if (page is ICacheable)
            stack.Push(page as ICacheable);
        }

        public void Clear()
        {
            stack.Clear();
        }

        
    }
}
