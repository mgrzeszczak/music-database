using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Cors;
using System.Web.Http.Dispatcher;
using Backend.Config;
using Common.Model;
using Common.Model.Constraints;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using NHibernate.Validator.Event;
using WebApi.App_Start;
using WebApi.Filters;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var storeCfg = new ModelConfig();
            var fluentConfig = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(c => c.Is(DatabaseConfig.ConnectionString)))
                .Mappings(m => {
                    var map = AutoMap.AssemblyOf<Song>(storeCfg);
                    map.UseOverridesFromAssemblyOf<SongConstraints>();
                    map.Conventions.Add(FluentNHibernate.Conventions.Helpers.DefaultLazy.Never());
                    m.AutoMappings.Add(map);
                }
                )
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true));
            var conf = fluentConfig.BuildConfiguration();
            var sessionFactory = conf.BuildSessionFactory();
            

            config.Filters.Add(new ExceptionFilter());
            config.Filters.Add(new AuthorizationFilter());
            config.Filters.Add(new ValidationFilter());
            config.Filters.Add(new TransactionFilter(sessionFactory));
            

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
            config.MapHttpAttributeRoutes(new CentralizedPrefixProvider("api"));

            var corsAttr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsAttr);

            //config.Services.Replace(typeof(IHttpControllerSelector), new HttpNotFoundAwareDefaultHttpControllerSelector(config));
            //config.Services.Replace(typeof(IHttpActionSelector), new HttpNotFoundAwareControllerActionSelector());

            config.Routes.MapHttpRoute(
                name: "Default",
                //routeTemplate: "{*url}",
                routeTemplate: "{*.*}",
                defaults: new { controller = "Error", action = "NotFound" }
            );
            /*
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );*/
        }
    }
}
