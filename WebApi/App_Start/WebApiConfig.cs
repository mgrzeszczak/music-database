
using Backend.Config;
using Common.Model;
using Common.Model.Constraints;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System.Net.Http.Headers;
using System.Web.Http;
using WebApi.App_Start;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
                        
            // Web API routes
            config.MapHttpAttributeRoutes();
            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));

            var storeCfg = new ModelConfig();
            var fluentConfig = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(c => c.Is(DatabaseConfig.ConnectionString)))
                .Mappings(m => m.AutoMappings.Add(AutoMap.AssemblyOf<Song>(storeCfg).UseOverridesFromAssemblyOf<SongConstraints>()))
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true));
            var conf = fluentConfig.BuildConfiguration();
            var sessionFactory = conf.BuildSessionFactory();

            GlobalConfiguration.Configuration.Filters.Add(new RequestFilter(sessionFactory));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
