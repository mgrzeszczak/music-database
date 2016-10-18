using Backend.Communication;
using Common.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop
{
    public sealed class DataAccessor
    {
        private static volatile DataAccessor instance;
        private static object Lock = new Object();
        public IDataProvider DataProvider { get; private set; }

        private DataAccessor(IDataProvider dataProvider) {
            this.DataProvider = dataProvider;
        }

        public static DataAccessor Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (Lock)
                    {
                        if (instance == null)
                            instance = new DataAccessor(new BackendDataProvider());
                    }
                }

                return instance;
            }
        }
    }
}
