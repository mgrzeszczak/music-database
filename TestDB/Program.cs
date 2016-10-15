using Common.Model;
using Common.NHibernate;
using Common.Repository;
using Common.Service;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var storeCfg = new StoreConfiguration();
            var fluentConfig = Fluently.Configure().Database(
             MySQLConfiguration.Standard.ConnectionString(
                 c => c.Is("Server=localhost;Database=lyrics;User=root;Password=root;")))
             .Mappings(m =>m.AutoMappings.Add(AutoMap.AssemblyOf<Song>(storeCfg)))
             //.ProxyFactoryFactory("NHibernate.Bytecode.DefaultProxyFactoryFactory, NHibernate")
             .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true));
            
            var conf = fluentConfig.BuildConfiguration();

            /*
            foreach (var _class in conf.ClassMappings)
            {
                _class.DynamicUpdate = true;
            }*/

            var sessionFactory = conf.BuildSessionFactory();

            IUnitOfWork unitOfWork = new UnitOfWork(sessionFactory);
            Context.setUnitOfWork(unitOfWork);
            try
            {
                SongService songService = new SongService(new SongRepository());
                songService.test();
                //var song = Context.provideSession().Get<Song>(8L);
                //song.Title = "blarg112";
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                unitOfWork.Rollback();
            }

            try
            {
                unitOfWork.Commit();
            }
            catch
            {

            }
            


            
            
        }
    }
}
