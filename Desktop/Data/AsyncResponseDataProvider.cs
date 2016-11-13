using Common.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Domain;
using Common.Model;

namespace Desktop.Data
{
    public static class AsyncResponseDataProvider
    {
        public static async Task ExecuteAsynchronous(this IResponseDataProvider dataProvider,Action task)
        {
            await Task.Run(() => task());
        }   
    }
}
