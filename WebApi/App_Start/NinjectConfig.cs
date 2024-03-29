﻿using Backend.Repository;
using Backend.Service;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dependencies;
using Backend.Repository.Implementation;
using Backend.Repository.Interface;
using Backend.Service.Implementation;
using Backend.Service.Interface;
using NHibernate.Validator.Engine;
using NHibernate.Validator.Event;
using Ninject;
using WebApi.Authorization;
using WebApi.Controllers;

//  A small Library to configure Ninject (A Dependency Injection Library) with a WebAPI Application. 
//  To configure, take the following steps.
// 
//  1. Install Packages Ninject and Ninject.Web.Common 
//  2. Remove NinjectWebCommon.cs in your App_Start Directory
//  3. Add this file to your project  (preferrably in the App_Start Directory)  
//  4. Add Your Bindings to the Load method of MainModule. 
//     You can add as many additional modules to keep things organized
//     simply add them to the Modules property of the NinjectModules class
//  5. Add the following Line to your Global.asax
//          NinjectHttpContainer.RegisterModules(NinjectHttpModules.Modules);  
//  5b.To Automatically Register all NinjectModules in the Current Project, You should instead add
//          NinjectContainer.RegisterAssembly()
//  You are done. 

namespace WebApi.App_Start
{
    /// <summary>
    /// Resolves Dependencies Using Ninject
    /// </summary>
    public class NinjectHttpResolver : IDependencyResolver, IDependencyScope
    {
        public IKernel Kernel { get; private set; }
        public NinjectHttpResolver(params NinjectModule[] modules)
        {
            Kernel = new StandardKernel(modules);
        }

        public NinjectHttpResolver(Assembly assembly)
        {
            Kernel = new StandardKernel();
            Kernel.Load(assembly);
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
            //Do Nothing
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }


    // List and Describe Necessary HttpModules
    // This class is optional if you already Have NinjectMvc
    public class NinjectHttpModules
    {
        //Return Lists of Modules in the Application
        public static NinjectModule[] Modules
        {
            get
            {
                return new[] { new MainModule() };
            }
        }

        //Main Module For Application
        public class MainModule : NinjectModule
        {
            public override void Load()
            {
                this.Bind<ISongRepository>().To<SongRepository>();
                this.Bind<IArtistRepository>().To<ArtistRepository>();
                this.Bind<IAlbumRepository>().To<AlbumRepository>();

                this.Bind<IAlbumService>().To<AlbumService>();
                this.Bind<IArtistService>().To<ArtistService>();
                this.Bind<ISongService>().To<SongService>();

                this.Bind<IUserCache>().To<UserCache>().InSingletonScope();

                
                this.Bind<IUserService>().To<UserService>();
                this.Bind<IUserRepository>().To<UserRepository>();

                this.Bind<ICommentService>().To<CommentService>();
                this.Bind<ICommentRepository>().To<CommentRepository>();

                this.Bind<IRatingService>().To<RatingService>();
                this.Bind<IRatingRepository>().To<RatingRepository>();

                this.Bind<IAuthorizationService>().To<AuthorizationService>();

                this.Bind<NHibernateSharedEngineProvider>().ToSelf().InSingletonScope();
                //TODO: Bind to Concrete Types Here
            }
        }
    }


    /// <summary>
    /// Its job is to Register Ninject Modules and Resolve Dependencies
    /// </summary>
    public class NinjectHttpContainer
    {
        private static NinjectHttpResolver _resolver;

        //Register Ninject Modules
        public static void RegisterModules(NinjectModule[] modules)
        {
            _resolver = new NinjectHttpResolver(modules);
            GlobalConfiguration.Configuration.DependencyResolver = _resolver;
        }

        public static void RegisterAssembly()
        {
            _resolver = new NinjectHttpResolver(Assembly.GetExecutingAssembly());
            //This is where the actual hookup to the Web API Pipeline is done.
            GlobalConfiguration.Configuration.DependencyResolver = _resolver;
        }

        //Manually Resolve Dependencies
        public static T Resolve<T>()
        {
            return _resolver.Kernel.Get<T>();
        }
    }
}