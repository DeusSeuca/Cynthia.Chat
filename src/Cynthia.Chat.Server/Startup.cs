using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cynthia.Chat.Server.Attributes;
using System.Reflection;
using Cynthia.Test.Chat.Attributes;
using Cynthia.Chat.Server.Services;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Cynthia.Chat.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public static IContainer ApplicationContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            var myControllers = assembly.ExportedTypes.Where(x => x.Name.EndsWith("Controller") && x.IsClass && !x.IsAbstract && !x.ContainsGenericParameters).ToArray();
            var myServices = assembly.DefinedTypes.Where(x => (x.IsDefined(typeof(ServiceAttribute)))
            || (!x.IsDefined(typeof(NonServiceAttribute)) && x.Name.EndsWith("Service") && x.IsClass && !x.IsAbstract && !x.ContainsGenericParameters)).ToArray();

            services.AddMvc().AddControllersAsServices();
            var builder = new ContainerBuilder();
            builder.Populate(services);

            //控制器
            builder.RegisterTypes(myControllers).PropertiesAutowired().AsSelf();
            //服务
            //保险起见还是用这样的url
            builder.RegisterType<MongoClient>().WithParameter(new NamedParameter("connectionString", "mongodb://localhost:27017")).AsImplementedInterfaces().PropertiesAutowired().AsSelf();
            //builder.Register<MongoClient>(x => new MongoClient("mongodb://localhost:27017")).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(myServices.Where(x => x.IsDefined(typeof(SingletonAttribute))).ToArray()).AsSelf().AsImplementedInterfaces().PropertiesAutowired().SingleInstance();
            builder.RegisterTypes(myServices.Where(x => x.IsDefined(typeof(TransientAttribute))).ToArray()).AsSelf().AsImplementedInterfaces().PropertiesAutowired().InstancePerDependency();
            builder.RegisterTypes(myServices.Where(x => x.IsDefined(typeof(ScopedAttribute))).ToArray()).AsSelf().AsImplementedInterfaces().PropertiesAutowired().InstancePerLifetimeScope();

            ApplicationContainer = builder.Build();
            services.AddMvc();
            var mda = ApplicationContainer.IsRegistered<DataService>();
            var minit = ApplicationContainer.IsRegistered<InitializationService>();
            var mmongo = ApplicationContainer.IsRegistered<MongoClient>();
            ApplicationContainer.Resolve<InitializationService>().Start();

            return new AutofacServiceProvider(ApplicationContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
