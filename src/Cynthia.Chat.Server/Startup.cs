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
using System.Reflection;
using Cynthia.Chat.Server.Services;
using MongoDB.Driver;
using MongoDB.Bson;
using Cynthia.Chat.Server.Controllers;
using Cynthia.Chat.Common;
using Cynthia.Chat.Common.Attributes;
using Alsein.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cynthia.Chat.Server.Hubs;

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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().AddControllersAsServices();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSignalR();
            var builder = new ContainerBuilder();
            builder.Populate(services);
            var assemblys = AssemblyManager.AllModuleAssemblies.ToArray();
            //控制器
            builder.RegisterAssemblyTypes(assemblys)
                .Where(x => x.Name.EndsWith("Controller"))
                .PropertiesAutowired();
            //服务
            builder.RegisterType<MongoClient>()
                .WithParameter("connectionString", "mongodb://cynthia.ovyno.com:27017")
                .As<IMongoClient>()
                .PropertiesAutowired()
                .AsSelf();
            builder.RegisterAssemblyTypes(assemblys)
                .Where(x => x.Name.EndsWith("Service") && x.IsDefined(typeof(SingletonAttribute)))
                .PreserveExistingDefaults()
                .PropertiesAutowired()
                .SingleInstance()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterAssemblyTypes(assemblys)
                .Where(x => x.Name.EndsWith("Service") && x.IsDefined(typeof(TransientAttribute)))
                .PreserveExistingDefaults()
                .PropertiesAutowired()
                .InstancePerDependency()
                .AsImplementedInterfaces()
                .AsSelf();
            builder.RegisterAssemblyTypes(assemblys)
                .Where(x => x.Name.EndsWith("Service") && x.IsDefined(typeof(ScopedAttribute)))
                .PreserveExistingDefaults()
                .PropertiesAutowired()
                .InstancePerLifetimeScope()
                .AsImplementedInterfaces()
                .AsSelf();

            ApplicationContainer = builder.Build();
            var isr = ApplicationContainer.IsRegistered<IDatabaseService>();
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
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("/chathub");
            });
            app.UseMvc();
        }
    }
}
