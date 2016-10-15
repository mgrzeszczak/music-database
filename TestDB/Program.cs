using Common.Model;
using Common.NHibernate;
using Desktop.Communication;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;

namespace TestDB
{
    class Program
    {
        static void Main(string[] args)
        {
            BackendDataProvider backendDataProvider = new BackendDataProvider();
            IList<Song> list = backendDataProvider.searchSongs("that");
            Console.WriteLine(list.Count);
        }
    }
}
