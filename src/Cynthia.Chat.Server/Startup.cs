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
using Cynthia.Chat.Server.Controllers;

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
            services.AddMvc().AddControllersAsServices();
            var builder = new ContainerBuilder();
            builder.Populate(services);

            //控制器
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(x => x.Name.EndsWith("Controller"))
                .PropertiesAutowired();
            //服务
            builder.RegisterType<MongoClient>()
                .WithParameter("connectionString", "mongodb://cynthia.ovyno.com:27017")
                .AsImplementedInterfaces()
                .PropertiesAutowired()
                .AsSelf();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(x => x.Name.EndsWith("Service") && x.IsDefined(typeof(SingletonAttribute)))
                .PropertiesAutowired()
                .SingleInstance()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(x => x.Name.EndsWith("Service") && x.IsDefined(typeof(TransientAttribute)))
                .PropertiesAutowired()
                .InstancePerDependency()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                .Where(x => x.Name.EndsWith("Service") && x.IsDefined(typeof(ScopedAttribute)))
                .PropertiesAutowired()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces()
                .AsSelf();

            ApplicationContainer = builder.Build();
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
